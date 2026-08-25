namespace WordVoca.Core.Models;

public class Word
{
    public required Guid Id { get; init; }

    public string? Value { get; set; }

    public string? Note { get; set; }

    public string? Translation { get; set; }

    internal static class PrivateAccessor
    {
        public static Word Restore(
            Guid id,
            string? value,
            string? note,
            string? translation)
        {
            return new Word
            {
                Id = id,
                Value = value,
                Note = note,
                Translation = translation
            };
        }
    }
}
