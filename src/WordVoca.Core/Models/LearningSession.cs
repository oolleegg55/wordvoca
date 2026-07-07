namespace WordVoca.Core.Models;

public class LearningSession
{
    public LearningSession(IReadOnlyList<Word> words)
    {
        Words = words;
    }

    public Word? CurrentWord { get; set; }

    public IReadOnlyList<Word> Words { get; }

    public int CurrentIndex { get; set; } = 0;

    public int WordsCount => Words.Count;

    public bool IsLastWord => CurrentIndex == WordsCount - 1;

    public bool TryChangeWordToNext()
    {
        if (!Words.Any())
        {
            return false;
        }

        if (CurrentWord is null && CurrentIndex == default)
        {
            CurrentWord = Words[CurrentIndex];
            return true;
        }

        var index = CurrentIndex + 1;

        if (index < WordsCount)
        {
            CurrentIndex++;
            CurrentWord = Words[CurrentIndex];
            return true;
        }

        return false;
    }

    public bool TryChangeWordToPrevious()
    {
        var index = CurrentIndex - 1;

        if (index >= 0 || index < WordsCount)
        {
            CurrentIndex--;
            CurrentWord = Words[index];

            return true;
        }

        return false;
    }
}
