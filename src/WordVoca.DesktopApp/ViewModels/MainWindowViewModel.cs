
using System;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.DependencyInjection;

using WordVoca.DesktopApp.Models;

namespace WordVoca.DesktopApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IDisposable
{
    private readonly IMessenger _messenger;

    public MainWindowViewModel(
        IMessenger messenger,
        IServiceProvider serviceProvider)
    {
        _messenger = messenger;
        CurrentPage = serviceProvider.GetRequiredService<MainViewModel>();

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
        CurrentPage = message.Value;
    }
}
