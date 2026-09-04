using Avalonia.Controls;

using WordVoca.DesktopApp.ViewModels.Dialogs;

namespace WordVoca.DesktopApp.Views.Dialogs;

public partial class AddWordView : Window
{
    public AddWordView()
    {
        InitializeComponent();
    }

    private async void HandleWindowLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is AddWordViewModel viewModel)
        {
            await viewModel.InitializeAsync();
        }
    }
}
