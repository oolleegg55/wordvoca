using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;
using WordVoca.DesktopApp.Models;
using WordVoca.DesktopApp.Services;
using WordVoca.DesktopApp.ViewModels.Dialogs;
using WordVoca.DesktopApp.Views.Dialogs;

namespace WordVoca.DesktopApp.ViewModels.Pages;

public partial class MainViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private readonly IWordListRepository _wordListStorage;
    private readonly IMessenger _messenger;

    public MainViewModel()
    {
        _dialogService = null!;
        _wordListStorage = null!;
        _messenger = null!;
    }

    public MainViewModel(
        IDialogService dialogService,
        IWordListRepository wordListStorage,
        IMessenger messenger)
    {
        _dialogService = dialogService;
        _wordListStorage = wordListStorage;
        _messenger = messenger;
    }

    public async Task InitializeAsync()
    {
        await LoadWordListAsync();
    }

    [ObservableProperty]
    private ObservableCollection<WordList> _wordLists = [];

    [RelayCommand]
    private async Task ShowCreationModalView()
    {
        await _dialogService.ShowModalAsync<CreationWordListView, CreationWordListViewModel>();

        await InitializeAsync();
    }

    [RelayCommand]
    private void ShowWordListDetail(string wordListId)
    {
        _messenger.Send(new NavigationMessage<WordListViewModel>(wordListId));
    }

    private async Task LoadWordListAsync()
    {
        List<WordList> wordLists = (await _wordListStorage.GetAllAsync())
            .OrderByDescending(x => x.CreatedAt)
            .ToList();

        WordLists.Clear();

        foreach (WordList wordList in wordLists)
        {
            WordLists.Add(wordList);
        }
    }
}
