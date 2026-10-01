using System;

using Avalonia.Controls;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Services;

public class PageFactory
{
    private readonly Func<Type, ViewModelBase> _factory;

    public PageFactory(Func<Type, ViewModelBase> factory)
    {
        _factory = factory;
    }

    public ViewModelBase GetPageViewModel<TViewModel>(Action<TViewModel>? afterCreation = null)
        where TViewModel : ViewModelBase
    {
        ViewModelBase viewModel = _factory(typeof(TViewModel));

        afterCreation?.Invoke((TViewModel)viewModel);

        return viewModel;
    }
}

public class DialogViewFactory
{
    private readonly Func<Type, Window> _factory;

    public DialogViewFactory(Func<Type, Window> factory)
    {
        _factory = factory;
    }

    public Window Create<TViewModel>()
        where TViewModel : DialogViewModel
    {
        return _factory(typeof(TViewModel));
    }
}
