using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;

namespace WordVoca.App.Pages.Exercises;

[QueryProperty(nameof(LearningSession), nameof(LearningSession))]
public partial class WordCardsExerciseVm : ObservableObject
{
    private CancellationTokenSource _cts = new();

    private readonly ITextToSpeech _textToSpeech;

    public WordCardsExerciseVm(ITextToSpeech textToSpeech)
    {
        _textToSpeech = textToSpeech;
    }

    public async Task InitializeAsync()
    {
        LearningSession.TryChangeWordToNext();
        CurrentWord = LearningSession.CurrentWord;
        HasNote = !string.IsNullOrEmpty(CurrentWord?.Note);
    }

    public LearningSession LearningSession { get; set; } = null!;

    [ObservableProperty]
    private Word? _currentWord;

    [ObservableProperty]
    private bool _isLastWord = false;

    [ObservableProperty]
    private bool _isNotFirstWord = false;

    [ObservableProperty]
    private bool _isNotLastWord = true;

    [ObservableProperty]
    private bool _hasNote = false;

    [RelayCommand]
    private void MoveToNextWord()
    {
        if (LearningSession.TryChangeWordToNext())
        {
            CurrentWord = LearningSession.CurrentWord;
            HasNote = !string.IsNullOrEmpty(CurrentWord?.Note);
            IsNotFirstWord = true;

            if (LearningSession.IsLastWord)
            {
                IsLastWord = true;
                IsNotLastWord = false;
            }
        }
    }

    [RelayCommand]
    private void MoveToPreviousWord()
    {
        if (LearningSession.TryChangeWordToPrevious())
        {
            CurrentWord = LearningSession.CurrentWord;
            HasNote = !string.IsNullOrEmpty(CurrentWord?.Note);
            IsLastWord = false;
            IsNotLastWord = true;

            if (LearningSession.CurrentIndex == 0)
            {
                IsNotFirstWord = false;
            }
        }
    }

    [RelayCommand]
    private async Task PlayAudioAsync()
    {
        if (LearningSession.CurrentWord is null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(LearningSession.CurrentWord.Value))
        {
            return;
        }

        _cts?.Cancel();
        _cts?.Dispose();

        _cts = new CancellationTokenSource();

        await _textToSpeech.SpeakAsync(LearningSession.CurrentWord.Value, cancelToken: _cts.Token);
    }
}
