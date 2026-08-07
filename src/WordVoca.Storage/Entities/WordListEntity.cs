using WordVoca.Core.Models;

namespace WordVoca.Storage.Entities;

internal class WordListEntity
{
    public static WordListEntity From(WordList wordList)
    {
        return new WordListEntity
        {
            Id = wordList.Id,
            Name = wordList.Name,
            SourceLang = wordList.SourceLang,
            TargetLang = wordList.TargetLang,
            CreatedAt = wordList.CreatedAt,
            UpdatedAt = wordList.UpdatedAt,
            Words = [.. wordList.Words.Select(WordEntity.From)]
        };
    }

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Langs SourceLang { get; set; }

    public Langs TargetLang { get; set; }

    public List<WordEntity> Words { get; set; } = [];

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
