using System.Threading.Tasks;

using Avalonia.Controls;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Views;

public partial class MainView : ContentPage
{
    public MainView()
    {
        InitializeComponent();
    }

    private async void Root_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            await viewModel.LoadWordListAsync();
        }
    }
}
