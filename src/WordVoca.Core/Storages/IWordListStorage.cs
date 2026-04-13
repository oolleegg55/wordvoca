using WordVoca.Core.Models;

namespace WordVoca.Core.Storages;

public interface IWordListStorage
{
    void Save(WordList wordList);

    WordList GetById(Guid wordListId);

    List<WordList> GetAll();

    void AddWords(Guid id, List<Word> words);
}
