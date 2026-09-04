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

public class DialogViewModel<TResult> : DialogViewModel
{
    public TResult? Result { get; private set; }

    public void Close(TResult result)
    {
        Result = result;
        OnCloseCallback();
    }
}
