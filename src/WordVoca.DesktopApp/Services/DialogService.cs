using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Services;

public class DialogService : IDialogService
{
    private readonly Dictionary<ViewModelBase, Window> _openedWindows = new(ReferenceEqualityComparer.Instance);
    private readonly DialogViewFactory _dialogViewFactory;
    private readonly PageFactory _pageFactory;

    public DialogService(PageFactory pageFactory, DialogViewFactory dialogViewFactory)
    {
        _pageFactory = pageFactory;
        _dialogViewFactory = dialogViewFactory;
    }

    private Window GetOwner(ViewModelBase? parent)
    {
        if (parent is not null
            && _openedWindows.TryGetValue(parent, out Window? parentWindow))
        {
            return parentWindow;
        }

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            return desktop.MainWindow ?? throw new InvalidOperationException("Application lifetime is not IClassicDesktopStyleApplicationLifetime");
        }

        throw new InvalidOperationException("Main window is unavailable.");
    }

    private async Task<TResult> ShowModalCoreAsync<TViewModel, TResult>(
        ViewModelBase? parent,
        Action<TViewModel>? afterCreation,
        Func<TViewModel, TResult> getResult)
        where TViewModel : DialogViewModel
    {
        Window owner = GetOwner(parent);

        TViewModel viewModel = (TViewModel)_pageFactory.GetPageViewModel(afterCreation);

        Window view = _dialogViewFactory.Create<TViewModel>();
        view.DataContext = viewModel;
        view.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        void CloseHandler() => view.Close();

        viewModel.CloseCallback += CloseHandler;
        _openedWindows.Add(viewModel, view);

        try
        {
            await view.ShowDialog(owner);
            return getResult(viewModel);
        }
        finally
        {
            _openedWindows.Remove(viewModel);
            viewModel.CloseCallback -= CloseHandler;
        }
    }

    public async Task ShowModalAsync<TViewModel>(
        ViewModelBase? parent = null,
        Action<TViewModel>? afterCreation = null)
        where TViewModel : DialogViewModel
    {
        await ShowModalCoreAsync(
            parent,
            afterCreation,
            _ => true);
    }

    public Task<TResult> ShowModalAsync<TViewModel, TResult>(
        ViewModelBase? parent = null,
        Action<TViewModel>? afterCreation = null)
        where TViewModel : DialogViewModel<TResult>
    {
        return ShowModalCoreAsync(
            parent,
            afterCreation,
            viewModel => viewModel.Result!);
    }
}
