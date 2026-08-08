using WordVoca.Core.Models;

namespace WordVoca.Core.Storages;

public interface IWordListRepository
{
    WordList BuildWordList(string name, Langs sourceLang, Langs targetLang);

    Task SaveAsync(WordList wordList);

    Task<WordList?> GetByIdAsync(string wordListName);

    Task<List<WordList>> GetAllAsync();

    Task<string> GetNextWordListNameAsync();
}
