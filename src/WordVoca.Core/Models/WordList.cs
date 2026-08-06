namespace WordVoca.Core.Models;

public class WordList
{
    private readonly List<Word> _words = [];

    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public Langs SourceLang { get; set; }

    public Langs TargetLang { get; set; }

    public IReadOnlyList<Word> Words => _words;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public int WordsCount => Words.Count;

    public void AddWord(Word word)
    {
        if (string.IsNullOrWhiteSpace(word.Value)
            || string.IsNullOrWhiteSpace(word.Note)
            || string.IsNullOrWhiteSpace(word.Translation))
        {
            throw new ArgumentException("Word properties cannot be null or whitespace.");
        }

        _words.Add(word);
    }
}
