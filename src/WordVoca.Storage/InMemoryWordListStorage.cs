using System.Collections.Concurrent;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.Storage;

public class InMemoryWordListStorage : IWordListStorage
{
    private readonly List<WordList> _wordLists = new();

    public async Task Save(WordList wordList)
    {
        wordList.CreatedAt = DateTime.Now;
        wordList.UpdatedAt = DateTime.Now;

        _wordLists.Add(wordList);
    }

    public async Task<List<WordList>> GetAll()
    {
        return _wordLists;
    }

    public async Task<WordList> GetById(Guid wordListId)
    {
        return _wordLists.First(x => x.Id == wordListId);
    }

    public async Task AddWord(Guid id, Word word)
    {
        _wordLists.First(x => x.Id == id).Words.Add(word);
    }
}
