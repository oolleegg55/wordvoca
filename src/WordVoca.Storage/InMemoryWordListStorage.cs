using System.Collections.Concurrent;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.Storage;

public class InMemoryWordListStorage : IWordListStorage
{
    private readonly ConcurrentDictionary<Guid, WordList> _wordLists = new();

    public void Save(WordList wordList)
    {
        wordList.CreatedAt = DateTime.Now;
        wordList.UpdatedAt = DateTime.Now;

        _wordLists.TryAdd(wordList.Id, wordList);
    }

    public List<WordList> GetAll()
    {
        return _wordLists.Values.ToList();
    }

    public void GetById(string wordListId)
    {
        throw new NotImplementedException();
    }
}
