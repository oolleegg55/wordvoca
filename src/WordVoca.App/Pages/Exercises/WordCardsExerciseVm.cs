using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;

namespace WordVoca.App.Pages.Exercises;

[QueryProperty(nameof(LearningSession), nameof(LearningSession))]
public partial class WordCardsExerciseVm : ObservableObject
{
    private CancellationTokenSource _cts = new();

    [ObservableProperty]
    private LearningSession _learningSession = null!;

    [ObservableProperty]
    private bool _isLastWord = false;

    public bool IsFirstWord => LearningSession.CurrentIndex == 0;

    [RelayCommand]
    private void MoveToNextWord()
    {
        if (LearningSession.CanMoveToNext())
        {
            IsLastWord = true;
        }
    }

    [RelayCommand]
    private void MoveToPreviousWord()
    {
        LearningSession.CanMoveToPrevious();
    }

    [RelayCommand]
    private async Task PlayAudioAsync()
    {
        if (LearningSession.CurrentWord.Value is null)
        {
            return;
        }

        _cts?.Cancel();
        _cts?.Dispose();

        _cts = new CancellationTokenSource();

        await TextToSpeech.Default.SpeakAsync(LearningSession.CurrentWord.Value, cancelToken: _cts.Token);
    }
}
