using Avalonia.Controls;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp;

public partial class CreationWordListView : Window
{
    public CreationWordListView()
    {
        InitializeComponent();
    }

    private async void Window_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is CreationWordListViewModel viewModel)
        {
            await viewModel.ChangeDefaultWordListTitle();
        }
    }
}
