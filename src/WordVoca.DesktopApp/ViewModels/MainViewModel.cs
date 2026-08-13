using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.DependencyInjection;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;
using WordVoca.DesktopApp.Models;
using WordVoca.DesktopApp.Services;

namespace WordVoca.DesktopApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private readonly IWordListStorage _wordListStorage;
    private readonly IMessenger _messenger;
    private readonly IServiceProvider _serviceProvider;

    public MainViewModel()
    {
        _dialogService = null!;
        _wordListStorage = null!;
        WordListViewModel = null!;
        _messenger = null!;
        _serviceProvider = null!;
    }

    public MainViewModel(
        CreationWordListViewModel wordListViewModel,
        IDialogService dialogService,
        IWordListStorage wordListStorage,
        IMessenger messenger,
        IServiceProvider serviceProvider)
    {
        _wordListViewModel = wordListViewModel;
        _dialogService = dialogService;
        _wordListStorage = wordListStorage;
        _messenger = messenger;
        _serviceProvider = serviceProvider;
    }

    [ObservableProperty]
    private ObservableCollection<WordList> _wordLists = [];

    [ObservableProperty]
    private CreationWordListViewModel _wordListViewModel;

    [RelayCommand]
    private async Task ShowCreationModalView()
    {
        await _dialogService.ShowModalAsync(WordListViewModel);

        await LoadWordListAsync();
    }

    [RelayCommand]
    private void ShowWordListDetail()
    {
        var viewModel = _serviceProvider.GetRequiredService<WordListViewModel>();
        _messenger.Send(
            new NavigationMessage(
                viewModel
                )
            );
    }

    public async Task LoadWordListAsync()
    {
        var wordLists = (await _wordListStorage.GetAll()).OrderByDescending(x => x.CreatedAt);

        WordLists = new ObservableCollection<WordList>(wordLists);
    }
}
