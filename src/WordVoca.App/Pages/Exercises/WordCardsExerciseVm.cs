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
        CurrentWord = LearningSession.CurrentWord;
        HasNote = !string.IsNullOrEmpty(CurrentWord?.Note);
    }

    public LearningSession LearningSession { get; set; } = null!;

    [ObservableProperty]
    private Word _currentWord = null!;

    [ObservableProperty]
    private bool _isNotFirstWord = false;

    [ObservableProperty]
    private bool _isNotLastWord = true;

    [ObservableProperty]
    private bool _hasNote = false;

    [ObservableProperty]
    private string _nextButtonText = "Next word";

    [RelayCommand]
    private async Task MoveToNextWord()
    {
        if (LearningSession.TryChangeWordToNext())
        {
            CurrentWord = LearningSession.CurrentWord;
            HasNote = !string.IsNullOrEmpty(CurrentWord?.Note);
            IsNotFirstWord = true;

            if (LearningSession.IsLastWord)
            {
                IsNotLastWord = false;

                NextButtonText = "Next exercise";
            }
        }
        else
        {
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    private void MoveToPreviousWord()
    {
        if (LearningSession.TryChangeWordToPrevious())
        {
            CurrentWord = LearningSession.CurrentWord;
            HasNote = !string.IsNullOrEmpty(CurrentWord?.Note);
            IsNotLastWord = true;
            NextButtonText = "Next word";

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
