using CommunityToolkit.Mvvm.Messaging.Messages;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Models;

public class NavigationMessage : ValueChangedMessage<ViewModelBase>
{
    public NavigationMessage(ViewModelBase value) : base(value)
    {
    }
}
