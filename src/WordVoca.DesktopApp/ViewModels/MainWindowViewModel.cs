using System;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using WordVoca.Core.Models;

namespace WordVoca.DesktopApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
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
    }

    [ObservableProperty]
    private ObservableCollection<WordList> _wordLists = [];
}
