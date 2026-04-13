using System.Collections.Concurrent;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.Storage;

public class InMemoryWordListStorage : IWordListStorage
{
    private readonly List<WordList> _wordLists = new();

    public void Save(WordList wordList)
    {
        wordList.CreatedAt = DateTime.Now;
        wordList.UpdatedAt = DateTime.Now;

        _wordLists.Add( wordList);
    }

    public List<WordList> GetAll()
    {
        return _wordLists;
    }

    public WordList GetById(Guid wordListId)
    {
        return _wordLists.First(x => x.Id == wordListId);
    }

    public void AddWords(Guid id, List<Word> words)
    {
        foreach (Word item in words)
        {
            _wordLists.First(x => x.Id == id).Words.Add(item);
        }
    }
}
