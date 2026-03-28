using WordVoca.Core.Models;

namespace WordVoca.Core.Storages;

public interface IWordListStorage
{
    void Save(WordList wordList);

    List<WordList> GetAll();

    void GetById(string wordListId);
}
