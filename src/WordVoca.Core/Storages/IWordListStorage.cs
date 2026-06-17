using WordVoca.Core.Models;

namespace WordVoca.Core.Storages;

public interface IWordListStorage
{
    Task Save(WordList wordList);

    Task<WordList?> GetById(Guid wordListId);

    Task<List<WordList>> GetAll();

    Task AddWord(Guid id, Word words);
}
