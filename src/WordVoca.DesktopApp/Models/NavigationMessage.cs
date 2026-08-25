using CommunityToolkit.Mvvm.Messaging.Messages;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Models;

public sealed class NavigationMessage<TPage> : ValueChangedMessage<object?>
    where TPage : ViewModelBase
{
    public NavigationMessage(object? parameter = null) : base(parameter)
    {
    }
}
