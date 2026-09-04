using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using WordVoca.Core.Models;
using WordVoca.Core.Repositories;
using WordVoca.DesktopApp.Models;
using WordVoca.DesktopApp.Services;
using WordVoca.DesktopApp.ViewModels.Dialogs;

namespace WordVoca.DesktopApp.ViewModels.Pages;

public partial class WordListViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly IDialogService _dialogService;
    private readonly IWordListRepository _wordListRepository;

    public WordListViewModel()
    {
        _messenger = WeakReferenceMessenger.Default;
        _wordListRepository = null!;
        _dialogService = null!;
    }

    public WordListViewModel(
        IMessenger messenger,
        IDialogService dialogService,
        IWordListRepository wordListRepository)
    {
        _messenger = messenger;
        _wordListRepository = wordListRepository;
        _dialogService = dialogService;
    }

    public async Task InitializeAsync()
    {
        await ReloadDataAsync();
    }

    public string WordListId { get; set; } = string.Empty;

    public ObservableCollection<Word> Words { get; set; } = [];

    [ObservableProperty]
    private WordList? _wordList;

    [RelayCommand]
    private void GoBack()
    {
        _messenger.Send(new NavigationMessage<MainViewModel>());
    }

    [RelayCommand]
    private async Task AddWordAsync()
    {
        if (WordList is null)
        {
            return;
        }

        await _dialogService.ShowModalAsync<AddWordViewModel>();
        await ReloadDataAsync();
    }

    private async Task ReloadDataAsync()
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
}
