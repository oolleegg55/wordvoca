namespace WordVoca.Core.Models;

public class Word
{
    public required Guid Id { get; set; }

    public string? Value { get; set; }

    public string? Note { get; set; }

    public string? Translation { get; set; }
}
