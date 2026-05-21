using System.Text.Json;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.Storage;

public class JsonWordListStorage : IWordListStorage
{
    private readonly string _filePath;

    public JsonWordListStorage(string filePath)
    {
        _filePath = filePath;
    }

    public async Task AddWord(Guid id, Word word)
    {
        List<WordList> wordLists = await GetAll();
        WordList wordList = wordLists.First((x) => x.Id == id);

        wordList.Words.Add(word);
        await Save(wordList);
    }

    public async Task<List<WordList>> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            return new List<WordList>();
        }

        string wordList = await File.ReadAllTextAsync(_filePath);

        try
        {
            return JsonSerializer.Deserialize<List<WordList>>(wordList) ?? new List<WordList>();
        }
        catch (JsonException)
        {
            File.Delete(_filePath);
            return new List<WordList>();
        }
    }

    public async Task<WordList> GetById(Guid wordListId)
    {
        List<WordList> wordLists = await GetAll();

        return wordLists.First((x) => x.Id == wordListId);
    }

    public async Task Save(WordList wordList)
    {
        var wordLists = await GetAll();
        wordLists.Add(wordList);
        string data = JsonSerializer.Serialize(wordLists);
        await File.WriteAllTextAsync(_filePath, data);
    }
}
