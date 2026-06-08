using System.Text.Json;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.Storage;

public class JsonWordListStorage : IWordListStorage
{
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
        WordList? wordList = await GetById(id);
        if (wordList == null)
        {
            return;
        }

        wordList.Words.Add(word);
        await Save(wordList);
    }

    public async Task<List<WordList>> GetAll()
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

    public async Task<WordList?> GetById(Guid wordListId)
    {
        string fileName = $"{wordListId}.json";
        string path = Path.Combine(_directoryPath, fileName);

        if (!File.Exists(path))
        {
            return null;
        }

        string data = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<WordList>(data) ?? null;
    }

    public async Task Save(WordList wordList)
    {
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        string data = JsonSerializer.Serialize(wordList);
        string fileName = $"{wordList.Id}.json";

        string path = Path.Combine(_directoryPath, fileName);

        await File.WriteAllTextAsync(path, data);
    }
}
