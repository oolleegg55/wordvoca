using System;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using WordVoca.Core.Models;
using WordVoca.DesktopApp.Models;

namespace WordVoca.DesktopApp.ViewModels.Pages;

public partial class WordListViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;

    public WordListViewModel(IMessenger messenger)
    {
        _messenger = messenger;

        _wordList = new WordList()
        {
            Id = Guid.NewGuid(),
            Name = "Word List #2",
            SourceLang = Langs.En,
            TargetLang = Langs.Ru
        };
    }

    [ObservableProperty]
    private WordList _wordList;

    [RelayCommand]
    private void GoBack()
    {
        _messenger.Send(new NavigationMessage(typeof(MainViewModel)));
    }
}
