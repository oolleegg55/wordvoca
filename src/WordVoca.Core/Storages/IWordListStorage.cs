using WordVoca.Core.Models;

namespace WordVoca.Core.Storages;

public interface IWordListStorage
{
    Task SaveAsync(WordList wordList);

    Task<WordList?> GetByIdAsync(string wordListName);

    Task<List<WordList>> GetAllAsync();

    Task AddWordAsync(string wordListName, Word words);

    Task<string> GetNextWordListNameAsync();
}
