using System;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Services;

public class DialogService : IDialogService
{
    private readonly PageFactory _pageFactory;

    public DialogService(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
    }

    private Window? GetMainWindow()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            return desktop.MainWindow;
        }

        throw new InvalidOperationException("Application lifetime is not IClassicDesktopStyleApplicationLifetime");
    }

    public async Task ShowModalAsync<TView, TViewModel>()
        where TView : Window, new()
        where TViewModel : DialogViewModel
    {
        Window? parentWindow = GetMainWindow();
        if (parentWindow is null)
        {
            return;
        }

        TView view = new();
        TViewModel viewModel = (TViewModel)_pageFactory.GetPageViewModel<TViewModel>();

        view.DataContext = viewModel;
        view.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        void closeHandler()
        {
            view.Close();
        }

        viewModel.CloseCallback += closeHandler;

        try
        {
            await view.ShowDialog(parentWindow);
        }
        finally
        {
            viewModel.CloseCallback -= closeHandler;
        }
    }
}
