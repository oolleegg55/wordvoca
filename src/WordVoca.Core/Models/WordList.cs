namespace WordVoca.Core.Models;

public class WordList
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public Langs SourceLang { get; set; }

    public Langs TargetLang { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
