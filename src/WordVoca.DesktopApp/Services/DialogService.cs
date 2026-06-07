using System;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Services;

public class DialogService : IDialogService
{
    private Window? GetMainWindow()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            return desktop.MainWindow;
        }

        throw new InvalidOperationException("Не найдено активное десктопное окно.");
    }

    public async Task ShowModalAsync(ViewModelBase viewModelBase)
    {
        CreationWordListView modalView = new()
        {
            DataContext = viewModelBase,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
        };

        Window? parentWindow = GetMainWindow();
        if (parentWindow is null)
        {
            return;
        }

        Action? closeHandler = null;
        closeHandler = () =>
        {
            viewModelBase.CloseCallback -= closeHandler;
            modalView.Close();
        };

        viewModelBase.CloseCallback += closeHandler;

        await modalView.ShowDialog(parentWindow);
    }
}
