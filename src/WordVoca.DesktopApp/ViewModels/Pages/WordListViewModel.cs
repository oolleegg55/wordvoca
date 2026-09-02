using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using WordVoca.Core.Models;
using WordVoca.Core.Repositories;
using WordVoca.DesktopApp.Models;

namespace WordVoca.DesktopApp.ViewModels.Pages;

public partial class WordListViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly IWordListRepository _wordListRepository;

    public WordListViewModel()
    {
        _messenger = WeakReferenceMessenger.Default;
        _wordListRepository = null!;

        Words = [];
    }

    public WordListViewModel(IMessenger messenger, IWordListRepository wordListRepository)
    {
        _messenger = messenger;
        _wordListRepository = wordListRepository;
        Words = [];
    }

    public async Task InitializeAsync()
    {
        WordList = await _wordListRepository.GetByIdAsync(WordListId);

        if (WordList is null)
        {
            return;
        }

        foreach (Word word in WordList.Words)
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

        WordList.TryAddWord(word);
        Words.Add(word);
    }
}
