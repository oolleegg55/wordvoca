using System;

using CommunityToolkit.Mvvm.ComponentModel;

using WordVoca.Core.Models;

namespace WordVoca.DesktopApp.ViewModels;

public partial class WordListViewModel : ViewModelBase
{
    [ObservableProperty]
    private WordList _wordList;

    public WordListViewModel()
    {
        _wordList = new WordList()
        {
            Id = Guid.NewGuid(),
            Name = "Word List #2",
            SourceLang = Langs.En,
            TargetLang = Langs.Ru
        };
    }
}
