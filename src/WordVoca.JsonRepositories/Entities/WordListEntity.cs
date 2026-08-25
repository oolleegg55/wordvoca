using WordVoca.Core.Models;

namespace WordVoca.Storage.Entities;

internal sealed class WordListEntity
{
    public static WordListEntity From(WordList wordList)
    {
        return new WordListEntity
        {
            Id = wordList.Id,
            Name = wordList.Name,
            SourceLang = wordList.SourceLang.ToString(),
            TargetLang = wordList.TargetLang.ToString(),
            CreatedAt = wordList.CreatedAt,
            UpdatedAt = wordList.UpdatedAt,
            Words = [.. wordList.Words.Select(WordEntity.From)]
        };
    }

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string SourceLang { get; set; } = string.Empty;

    public string TargetLang { get; set; } = string.Empty;

    public List<WordEntity> Words { get; set; } = [];

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
