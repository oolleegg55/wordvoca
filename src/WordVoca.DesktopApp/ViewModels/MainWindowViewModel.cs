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

        _messenger.Register<MainWindowViewModel, NavigationMessage>(this, HandlerNavigationMessageRecieved);
    }

    public void Dispose()
    {
        _messenger.Unregister<NavigationMessage>(this);
    }

    [ObservableProperty]
    private ViewModelBase _currentPage;

    private void HandlerNavigationMessageRecieved(MainWindowViewModel recipient, NavigationMessage message)
    {
        CurrentPage = _pageFactory.GetPageViewModel(message.Value);
    }
}
