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
    private readonly IWordListStorage _wordListStorage;
    private readonly IMessenger _messenger;

    public MainViewModel()
    {
        _dialogService = null!;
        _wordListStorage = null!;
        _messenger = null!;
    }

    public MainViewModel(
        IDialogService dialogService,
        IWordListStorage wordListStorage,
        IMessenger messenger)
    {
        _dialogService = dialogService;
        _wordListStorage = wordListStorage;
        _messenger = messenger;
    }

    [ObservableProperty]
    private ObservableCollection<WordList> _wordLists = [];

    [RelayCommand]
    private async Task ShowCreationModalView()
    {
        await _dialogService.ShowModalAsync<CreationWordListView, CreationWordListViewModel>();

        await LoadWordListAsync();
    }

    [RelayCommand]
    private void ShowWordListDetail(WordList wordList)
    {
        _messenger.Send(new NavigationMessage<WordListViewModel>(wordList));
    }

    public async Task LoadWordListAsync()
    {
        List<WordList> wordLists = (await _wordListStorage.GetAll())
            .OrderByDescending(x => x.CreatedAt)
            .ToList();

        WordLists = new ObservableCollection<WordList>(wordLists);
    }
}
