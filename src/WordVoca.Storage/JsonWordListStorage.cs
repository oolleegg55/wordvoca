using System.Text.Json;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.Storage;

public class JsonWordListStorage : IWordListStorage
{
    private readonly static SemaphoreSlim s_semaphoreSlim = new(1, 1);
    private readonly string _directoryPath;

    public JsonWordListStorage(string directoryPath)
    {
        _directoryPath = directoryPath;
    }

    public JsonWordListStorage(StorageSettings storageSettings)
    {
        _directoryPath = storageSettings.StorageDirectory;
    }

    public async Task AddWord(Guid id, Word word)
    {
        await s_semaphoreSlim.WaitAsync();
        try
        {
            string path = GetFilePath(id);
            string text = await File.ReadAllTextAsync(path);

            WordList? wordList = JsonSerializer.Deserialize<WordList>(text);
            if (wordList == null)
            {
                return;
            }

            wordList.Words.Add(word);

            string data = JsonSerializer.Serialize(wordList);
            await File.WriteAllTextAsync(path, data);
        }
        finally
        {
            s_semaphoreSlim.Release();
        }
    }

    public async Task<List<WordList>> GetAll()
    {
        await s_semaphoreSlim.WaitAsync();

        try
        {
            var result = new List<WordList>();

            if (!Directory.Exists(_directoryPath))
            {
                return new();
            }

            foreach (var file in Directory.EnumerateFiles(_directoryPath, "*.json"))
            {
                var text = await File.ReadAllTextAsync(file);
                var wordList = JsonSerializer.Deserialize<WordList>(text);
                if (wordList != null)
                {
                    result.Add(wordList);
                }
            }

            return result;
        }
        finally
        {
            s_semaphoreSlim.Release();
        }
    }

    public async Task<WordList?> GetById(Guid wordListId)
    {
        await s_semaphoreSlim.WaitAsync();
        try
        {
            string path = GetFilePath(wordListId);

            if (!File.Exists(path))
            {
                return null;
            }

            string data = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<WordList>(data) ?? null;
        }
        finally
        {
            s_semaphoreSlim.Release();
        }
    }

    public async Task Save(WordList wordList)
    {
        await s_semaphoreSlim.WaitAsync();

        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string data = JsonSerializer.Serialize(wordList);
            string path = GetFilePath(wordList.Id);

            await File.WriteAllTextAsync(path, data);
        }
        finally
        {
            s_semaphoreSlim.Release();
        }
    }

    private string GetFilePath(Guid id)
    {
        return Path.Combine(_directoryPath, $"{id}.json");
    }
}
