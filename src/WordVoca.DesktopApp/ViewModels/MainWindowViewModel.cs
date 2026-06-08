using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;
using WordVoca.DesktopApp.Services;

namespace WordVoca.DesktopApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private readonly IWordListStorage _wordListStorage;

    public MainWindowViewModel()
    {

    }

    public MainWindowViewModel(CreationWordListViewModel wordListViewModel,
                               IDialogService dialogService,
                               IWordListStorage wordListStorage)
    {
        _wordListViewModel = wordListViewModel;
        _dialogService = dialogService;
        _wordListStorage = wordListStorage;
    }

    [ObservableProperty]
    private ObservableCollection<WordList> _wordLists = [];

    [ObservableProperty]
    private CreationWordListViewModel _wordListViewModel;

    [RelayCommand]
    private async Task ShowCreationModalView()
    {
        await _dialogService.ShowModalAsync(WordListViewModel);
    }

    public async Task LoadWordListAsync()
    {
        var wordLists = await _wordListStorage.GetAll();
        WordLists = new ObservableCollection<WordList>(wordLists);
    }
}
