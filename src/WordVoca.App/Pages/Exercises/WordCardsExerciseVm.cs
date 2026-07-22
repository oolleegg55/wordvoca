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
        if (LearningSession is null)
        {
            return;
        }

        CurrentWord = LearningSession.CurrentWord;
    }

    public LearningSession? LearningSession { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFirstWord))]
    [NotifyPropertyChangedFor(nameof(IsLastWord))]
    [NotifyPropertyChangedFor(nameof(HasNote))]
    private Word _currentWord = null!;

    public bool IsFirstWord => LearningSession?.IsFirstWord ?? false;

    public bool IsLastWord => LearningSession?.IsLastWord ?? false;

    public bool HasNote => !string.IsNullOrEmpty(CurrentWord?.Note);

    [RelayCommand]
    private async Task MoveToNextWord()
    {
        if (LearningSession is null)
        {
            return;
        }

        if (LearningSession.TryChangeWordToNext())
        {
            CurrentWord = LearningSession.CurrentWord;
        }
        else
        {
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    private void MoveToPreviousWord()
    {
        if (LearningSession is null)
        {
            return;
        }

        if (LearningSession.TryChangeWordToPrevious())
        {
            CurrentWord = LearningSession.CurrentWord;
        }
    }

    [RelayCommand]
    private async Task PlayAudioAsync()
    {
        if (CurrentWord is null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(CurrentWord.Value))
        {
            return;
        }

        _cts?.Cancel();
        _cts?.Dispose();

        _cts = new CancellationTokenSource();

        await _textToSpeech.SpeakAsync(CurrentWord.Value, cancelToken: _cts.Token);
    }
}
