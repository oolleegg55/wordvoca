using System;

namespace WordVoca.DesktopApp.ViewModels;

public class DialogViewModel : ViewModelBase
{
    public event Action? CloseCallback;

    public void OnCloseCallback()
    {
        CloseCallback?.Invoke();
    }
}
