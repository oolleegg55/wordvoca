using System;

using CommunityToolkit.Mvvm.Messaging.Messages;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Models;

public class NavigationMessage : ValueChangedMessage<Type>
{
    public NavigationMessage(Type value) : base(value)
    {
    }
}
