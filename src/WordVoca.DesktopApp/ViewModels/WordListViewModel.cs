using System;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.DependencyInjection;

using WordVoca.Core.Models;
using WordVoca.DesktopApp.Models;

namespace WordVoca.DesktopApp.ViewModels;

public partial class WordListViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly IServiceProvider _serviceProvider;

    public WordListViewModel(IMessenger messenger, IServiceProvider serviceProvider)
    {
        _messenger = messenger;
        _serviceProvider = serviceProvider;

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
        _messenger.Send(
            new NavigationMessage(
                _serviceProvider.GetRequiredService<MainViewModel>()
                )
            );
    }
}
