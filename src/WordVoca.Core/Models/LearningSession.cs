namespace WordVoca.Core.Models;

public class LearningSession
{
    public LearningSession(IReadOnlyList<Word> words, IReadOnlyList<ExerciseType> exerciseTypes)
    {
        if (!words.Any())
        {
            throw new ArgumentNullException(nameof(words), "Words list cannot be empty.");
        }

        if (!exerciseTypes.Any())
        {
            throw new ArgumentNullException(nameof(exerciseTypes), "Exercise types list cannot be empty.");
        }

        Words = words;
        ExerciseTypes = exerciseTypes;

        CurrentWord = words[0];
        CurrentExerciseType = exerciseTypes[0];
    }

    public IReadOnlyList<Word> Words { get; }

    public IReadOnlyList<ExerciseType> ExerciseTypes { get; }

    public ExerciseType CurrentExerciseType { get; private set; }

    public Word CurrentWord { get; private set; }

    public int CurrentIndex { get; private set; } = 0;

    public bool IsLastWord => CurrentIndex == Words.Count - 1;

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

        if (index < Words.Count)
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

        if (index >= 0 || index < Words.Count)
        {
            CurrentIndex--;
            CurrentWord = Words[index];

            return true;
        }

        return false;
    }

    public void Reset()
    {
        CurrentWord = Words[0];
        CurrentIndex = 0;
    }
}
