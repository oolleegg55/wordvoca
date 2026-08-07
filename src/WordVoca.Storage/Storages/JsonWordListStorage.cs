using System.Text.Json;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;
using WordVoca.Storage.Entities;

namespace WordVoca.Storage.Storages;

public class JsonWordListStorage : IWordListStorage
{
    private readonly static SemaphoreSlim s_semaphoreSlim = new(1, 1);
    private readonly string _directoryPath;

    public JsonWordListStorage(StorageSettings storageSettings)
    {
        _directoryPath = storageSettings.StorageDirectory;
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

            return $"Word List #{filesCount}";

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
            .Replace(" ", "-")
            .Replace("#", "no")
            .ToLowerInvariant();

        return Path.Combine(_directoryPath, $"{normalizedFileName}.json");
    }

    private WordList MapToDomain(WordListEntity entity)
    {
        return WordList.PrivateAccessor.Restore(
            entity.Id,
            entity.Name,
            entity.SourceLang,
            entity.TargetLang,
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
