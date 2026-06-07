using System;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;

namespace WordVoca.DesktopApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {

    }

    public MainWindowViewModel(CreationWordListViewModel wordListViewModel)
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
    }

    [ObservableProperty]
    private ObservableCollection<WordList> _wordLists = [];

    [ObservableProperty]
    private CreationWordListViewModel _wordListViewModel;

    [RelayCommand]
    private void ShowCreationModalView()
    {
        WordListViewModel.IsVisible = true;
    }
}
