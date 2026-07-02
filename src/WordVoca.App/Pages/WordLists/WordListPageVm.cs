using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.App.Pages.WordLists;

[QueryProperty(nameof(WordListId), "WordListId")]
public partial class WordListPageVm : ObservableObject
{
    private CancellationTokenSource _cts = new();

    private readonly IWordListStorage _wordListStorage;

    public WordListPageVm(IWordListStorage wordListStorage)
    {
        _wordListStorage = wordListStorage;
    }

    public async Task InitializeAsync()
    {
        WordList? wordList = await _wordListStorage.GetByIdAsync(WordListId);
        if (wordList is null)
        {
            return;
        }

        WordListName = wordList.Name;
        Words.Clear();

        foreach (var word in wordList.Words)
        {
            Words.Add(word);
        }
    }

    public string WordListId { get; set; } = string.Empty;

    [ObservableProperty]
    private string _wordListName = string.Empty;

    public ObservableCollection<Word> Words { get; } = [];

    [RelayCommand]
    private async Task GoToAddingWordsPageAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(AddingWordsPage)}?WordListId={WordListId}");
    }

    [RelayCommand(AllowConcurrentExecutions = true)]
    private async Task PlayWordAudioAsync(string word)
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();

        await TextToSpeech.Default.SpeakAsync(word, cancelToken: _cts.Token);
    }
}
