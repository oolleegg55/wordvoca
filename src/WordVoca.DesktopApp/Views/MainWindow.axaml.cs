using Avalonia.Controls;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Window_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel)
        {
            await viewModel.LoadWordListAsync();
        }
    }
}
