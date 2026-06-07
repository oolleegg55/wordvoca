using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.DesktopApp.Services;

namespace WordVoca.DesktopApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;

    public MainWindowViewModel()
    {

    }

    public MainWindowViewModel(CreationWordListViewModel wordListViewModel,
                               IDialogService dialogService)
    {
        _wordLists.Add(new WordList()
        {
            Id = Guid.NewGuid(),
            Name = "Unity",
            SourceLang = Langs.Ru,
            TargetLang = Langs.En,
        });

        _wordLists.Add(new WordList()
        {
            Id = Guid.NewGuid(),
            Name = "Unity #1",
            SourceLang = Langs.En,
            TargetLang = Langs.Ru,
        });

        _wordListViewModel = wordListViewModel;
        _dialogService = dialogService;
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
}
