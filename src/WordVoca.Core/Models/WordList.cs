namespace WordVoca.Core.Models;

public class WordList
{
    private readonly List<Word> _words = [];

    private WordList(List<Word> words)
    {
        _words = words;
    }

    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public Langs SourceLang { get; private set; }

    public Langs TargetLang { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset UpdatedAt { get; private set; }

    public IReadOnlyList<Word> Words => _words;

    public int WordsCount => Words.Count;

    public bool TryAddWord(Word word)
    {
        if (string.IsNullOrWhiteSpace(word.Value)
            && string.IsNullOrEmpty(word.Translation)
            && string.IsNullOrEmpty(word.Note))
        {
            return false;
        }

        _words.Add(word);
        return true;
    }

    public void ChangeLanguages(Langs sourceLang, Langs targetLang)
    {
        SourceLang = sourceLang;
        TargetLang = targetLang;
    }

    internal static class PrivateAccessor
    {
        public static WordList Build(
            Guid id,
            string name,
            Langs sourceLang,
            Langs targetLang,
            DateTimeOffset createdAt,
            DateTimeOffset updatedAt)
        {
            return new WordList([])
            {
                Id = id,
                Name = name,
                SourceLang = sourceLang,
                TargetLang = targetLang,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt
            };
        }

        public static WordList Restore(
            Guid id,
            string name,
            Langs sourceLang,
            Langs targetLang,
            List<Word> words,
            DateTimeOffset createdAt,
            DateTimeOffset updatedAt)
        {
            return new WordList(words)
            {
                Id = id,
                Name = name,
                SourceLang = sourceLang,
                TargetLang = targetLang,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt,
            };
        }
    }
}
