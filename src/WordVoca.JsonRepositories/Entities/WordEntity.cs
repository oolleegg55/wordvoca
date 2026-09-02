using WordVoca.Core.Models;

namespace WordVoca.Storage.Entities;

internal sealed class WordEntity
{
    public static WordEntity From(Word word)
    {
        return new WordEntity
        {
            Id = word.Id,
            Value = word.Value,
            Note = word.Note,
            Translation = word.Translation,
        };
    }

    public required Guid Id { get; set; }

    public string? Value { get; set; }

    public string? Note { get; set; }

    public string? Translation { get; set; }
}
