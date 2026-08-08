namespace WordVoca.Core.Models;

public class WordList
{
    private readonly List<Word> _words = [];

    public WordList(List<Word> words)
    {
        _words = words;
    }

    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public Langs SourceLang { get; set; }

    public Langs TargetLang { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public IReadOnlyList<Word> Words => _words;

    public int WordsCount => Words.Count;

    public void AddWord(Word word)
    {
        if (string.IsNullOrWhiteSpace(word.Value)
            && string.IsNullOrEmpty(word.Translation)
            && string.IsNullOrEmpty(word.Note))
        {
            throw new ArgumentException("Word must have at least one non-empty field (Value, Translation, or Note).", nameof(word));
        }

        _words.Add(word);
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
