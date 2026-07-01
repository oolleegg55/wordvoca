using WordVoca.Core.Models;

namespace WordVoca.Core.Storages;

public interface IWordListStorage
{
    Task Save(WordList wordList);

    Task<WordList?> GetById(string wordListName);

    Task<List<WordList>> GetAll();

    Task AddWordAsync(string wordListName, Word words);
}
