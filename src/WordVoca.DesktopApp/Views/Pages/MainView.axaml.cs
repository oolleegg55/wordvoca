using Avalonia.Controls;

using WordVoca.DesktopApp.ViewModels.Pages;

namespace WordVoca.DesktopApp.Views.Pages;

public partial class MainView : ContentPage
{
    public MainView()
    {
        InitializeComponent();
    }

    private async void HandlePageLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            await viewModel.InitializeAsync();
        }
    }
}
