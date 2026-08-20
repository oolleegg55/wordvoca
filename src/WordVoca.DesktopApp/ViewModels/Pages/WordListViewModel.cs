using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;
using WordVoca.DesktopApp.Models;

namespace WordVoca.DesktopApp.ViewModels.Pages;

public partial class WordListViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly IWordListStorage _wordListStorage;

    public WordListViewModel()
    {
        _messenger = WeakReferenceMessenger.Default;
        _wordListStorage = null!;
        Words = [];

        WordList = new()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            SourceLang = Langs.En,
            TargetLang = Langs.Es,
            Words =
            [
                new Word()
                {
                    Id = Guid.NewGuid(),
                    Value = "Word",
                    Translation = "Слово",
                    Note = "Пример"
                },
            ]
        };
    }

    public WordListViewModel(IMessenger messenger, IWordListStorage wordListStorage)
    {
        _messenger = messenger;
        _wordListStorage = wordListStorage;
        Words = [];
    }

    public async Task InitializeAsync()
    {
        WordList = await _wordListStorage.GetByIdAsync(WordListId);

        if (WordList is null)
        {
            return;
        }

        foreach (var word in WordList.Words)
        {
            Words.Add(word);
        }
    }

    public string WordListId { get; set; } = string.Empty;

    [ObservableProperty]
    private WordList? _wordList;

    public ObservableCollection<Word> Words { get; set; }

    [RelayCommand]
    private void GoBack()
    {
        _messenger.Send(new NavigationMessage<MainViewModel>());
    }

    [RelayCommand]
    private void AddWord()
    {
        if (WordList is null)
        {
            return;
        }

        Word word = new()
        {
            Id = Guid.NewGuid(),
            Value = "Word",
            Translation = "Слово",
            Note = "Пример"
        };

        WordList.Words.Add(word);
        Words.Add(word);
    }
}
