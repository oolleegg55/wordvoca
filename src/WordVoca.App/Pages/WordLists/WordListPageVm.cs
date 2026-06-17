using System.Collections.ObjectModel;
using System.Diagnostics;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.App.Pages.WordLists;

[QueryProperty(nameof(WordListIdString), "WordListId")]
public partial class WordListPageVm : ObservableObject
{
    public ObservableCollection<Word> Words { get; } = [];

    [ObservableProperty]
    private string _wordListName = string.Empty;

    [ObservableProperty]
    private Guid _wordListId = Guid.Empty;

    public string WordListIdString
    {
        set
        {
            if (Guid.TryParse(value, out var guid))
            {
                WordListId = guid;
            }
        }
    }

    private CancellationTokenSource _cts;

    private readonly IWordListStorage _wordListStorage;

    public WordListPageVm(IWordListStorage wordListStorage)
    {
        _wordListStorage = wordListStorage;
    }

    partial void OnWordListIdChanged(Guid value)
    {
        OnWordListIdChangedAsync(value);
    }

    private async Task OnWordListIdChangedAsync(Guid value)
    {
        if (value != Guid.Empty && _wordListStorage != null)
        {
            try
            {
                var wordList = await _wordListStorage.GetById(value);
                WordListName = wordList.Name;
                Words.Clear();
                foreach (var word in wordList.Words)
                {
                    Words.Add(word);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading word list: {ex.Message}");
            }
        }
    }

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

    [RelayCommand]
    private async Task LoadWords()
    {
        if (WordListId == Guid.Empty)
        {
            return;
        }

        var wordList = await _wordListStorage.GetById(WordListId);
        WordListName = wordList.Name;
        Words.Clear();

        foreach (Word word in wordList.Words)
        {
            Words.Add(word);
        }
    }
}
