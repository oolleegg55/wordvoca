namespace WordVoca.Core.Models;

public class LearningSession
{
    public LearningSession(IReadOnlyList<Word> words)
    {
        Words = words;
    }

    public IReadOnlyList<Word> Words { get; }

    public int CurrentIndex = 0;

    public Word? CurrentWord => CurrentIndex < WordsCount ? Words[CurrentIndex] : null;

    public int WordsCount => Words.Count;

    public bool CanMoveToNext()
    {
        return ++CurrentIndex < WordsCount;
    }

    public bool CanMoveToPrevious()
    {
        return --CurrentIndex < WordsCount;
    }
}
