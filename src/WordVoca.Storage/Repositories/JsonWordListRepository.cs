using System.Text.Json;

using WordVoca.Core.Exceptions;
using WordVoca.Core.Models;
using WordVoca.Core.Storages;
using WordVoca.Storage.Entities;

namespace WordVoca.Storage.Storages;

public class JsonWordListRepository : IWordListRepository
{
    private readonly static SemaphoreSlim s_semaphoreSlim = new(1, 1);
    private readonly string _directoryPath;
    private readonly TimeProvider _timeProvider;

    public JsonWordListRepository(StorageSettings storageSettings, TimeProvider timeProvider)
    {
        _directoryPath = storageSettings.StorageDirectory;
        _timeProvider = timeProvider;
    }

    public WordList BuildWordList(string name, Langs sourceLang, Langs targetLang)
    {
        DateTimeOffset dateTime = _timeProvider.GetLocalNow();

        return WordList.PrivateAccessor.Build(
            Guid.NewGuid(),
            name,
            sourceLang,
            targetLang,
            dateTime,
            dateTime);
    }

    public async Task<List<WordList>> GetAllAsync()
    {
        await s_semaphoreSlim.WaitAsync();

        try
        {
            List<WordList> result = [];

            if (!Directory.Exists(_directoryPath))
            {
                return [];
            }

            foreach (string file in Directory.EnumerateFiles(_directoryPath, "*.json"))
            {
                string text = await File.ReadAllTextAsync(file);

                WordListEntity? entity = JsonSerializer.Deserialize<WordListEntity>(text);
                if (entity is not null)
                {
                    result.Add(MapToDomain(entity));
                }
            }

            return result;
        }
        finally
        {
            s_semaphoreSlim.Release();
        }
    }

    public async Task<WordList?> GetByIdAsync(string wordListName)
    {
        await s_semaphoreSlim.WaitAsync();
        try
        {
            string path = GetNormalizedFilePath(wordListName);

            if (!File.Exists(path))
            {
                return null;
            }

            string data = await File.ReadAllTextAsync(path);

            WordListEntity? entity = JsonSerializer.Deserialize<WordListEntity>(data);

            return entity is null ? null : MapToDomain(entity);
        }
        finally
        {
            s_semaphoreSlim.Release();
        }
    }

    public async Task SaveAsync(WordList wordList)
    {
        await s_semaphoreSlim.WaitAsync();

        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string data = JsonSerializer.Serialize(WordListEntity.From(wordList));
            string path = GetNormalizedFilePath(wordList.Name);

            await File.WriteAllTextAsync(path, data);
        }
        finally
        {
            s_semaphoreSlim.Release();
        }
    }

    public async Task<string> GetNextWordListNameAsync()
    {
        await s_semaphoreSlim.WaitAsync();

        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                return "Word List #1";
            }

            int filesCount = Directory.EnumerateFiles(_directoryPath, "*.json").Count();

            return $"Word List #{filesCount + 1}";

        }
        finally
        {
            s_semaphoreSlim.Release();
        }
    }

    private string GetNormalizedFilePath(string name)
    {
        string normalizedFileName = name
            .Trim()
            .Replace(" ", string.Empty)
            .ToLowerInvariant();

        return Path.Combine(_directoryPath, $"{normalizedFileName}.json");
    }

    private WordList MapToDomain(WordListEntity entity)
    {
        if (!Enum.TryParse(entity.SourceLang, true, out Langs sourceLang))
        {
            throw new UnsupportedLanguageException();
        }

        if (!Enum.TryParse(entity.SourceLang, true, out Langs targetLang))
        {
            throw new UnsupportedLanguageException();
        }

        return WordList.PrivateAccessor.Restore(
            entity.Id,
            entity.Name,
            sourceLang,
            targetLang,
            [.. entity.Words.Select(MapWordToDomain)],
            entity.CreatedAt,
            entity.UpdatedAt);
    }

    private Word MapWordToDomain(WordEntity wordEntity)
    {
        return Word.PrivateAccessor.Restore(
            wordEntity.Id,
            wordEntity.Value,
            wordEntity.Note,
            wordEntity.Translation
        );
    }
}
