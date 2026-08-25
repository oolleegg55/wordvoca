using Avalonia.Controls;
using Avalonia.Interactivity;

using WordVoca.DesktopApp.ViewModels.Pages;

namespace WordVoca.DesktopApp.Views.Pages;

public partial class WordListView : ContentPage
{
    public WordListView()
    {
        InitializeComponent();
    }

    private async void HandleLoadedPage(object? sender, RoutedEventArgs e)
    {
        if (DataContext is WordListViewModel viewModel)
        {
            await viewModel.InitializeAsync();
        }
    }
}
