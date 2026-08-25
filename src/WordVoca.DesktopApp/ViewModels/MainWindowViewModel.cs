using System;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using WordVoca.DesktopApp.Models;
using WordVoca.DesktopApp.Services;
using WordVoca.DesktopApp.ViewModels.Pages;

namespace WordVoca.DesktopApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IDisposable
{
    private readonly IMessenger _messenger;
    private readonly PageFactory _pageFactory;

    public MainWindowViewModel(
        IMessenger messenger,
        PageFactory pageFactory)
    {
        _messenger = messenger;
        _pageFactory = pageFactory;

        CurrentPage = _pageFactory.GetPageViewModel<MainViewModel>();

        _messenger.Register<MainWindowViewModel, NavigationMessage<WordListViewModel>>(this, OpenWordListPage);
        _messenger.Register<MainWindowViewModel, NavigationMessage<MainViewModel>>(this, OpenMainPage);
    }

    public void Dispose()
    {
        _messenger.Unregister<NavigationMessage<WordListViewModel>>(this);
        _messenger.Unregister<NavigationMessage<MainViewModel>>(this);
    }

    [ObservableProperty]
    private ViewModelBase _currentPage;

    private void OpenWordListPage(
        MainWindowViewModel recipient,
        NavigationMessage<WordListViewModel> message)
    {
        CurrentPage = _pageFactory.GetPageViewModel<WordListViewModel>(afterCreation =>
        {
            if (message.Value is not null)
            {
                afterCreation.WordListId = (string)message.Value;
            }
        });
    }

    private void OpenMainPage(
        MainWindowViewModel recipient,
        NavigationMessage<MainViewModel> message)
    {
        CurrentPage = _pageFactory.GetPageViewModel<MainViewModel>();
    }
}
