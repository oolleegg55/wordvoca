using System;

using CommunityToolkit.Mvvm.ComponentModel;

namespace WordVoca.DesktopApp.ViewModels;

public abstract class ViewModelBase : ObservableObject
{
    public event Action? CloseCallback;

    public void OnCloseCallback()
    {
        CloseCallback?.Invoke();
    }
}
