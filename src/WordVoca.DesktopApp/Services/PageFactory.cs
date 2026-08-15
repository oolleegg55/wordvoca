using System;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Services;

public class PageFactory(Func<Type, ViewModelBase> factory)
{
    public ViewModelBase GetPageViewModel<T>(Action<T>? afterCreation = null)
        where T : ViewModelBase
    {
        ViewModelBase viewModel = factory(typeof(T));

        afterCreation?.Invoke((T)viewModel);

        return viewModel;
    }

    public ViewModelBase GetPageViewModel(Type type, Action<ViewModelBase>? afterCreation = null)
    {
        ViewModelBase viewModel = factory(type);

        afterCreation?.Invoke(viewModel);

        return viewModel;
    }
}
