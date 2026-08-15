using Avalonia.Controls;

using WordVoca.DesktopApp.ViewModels.Dialogs;

namespace WordVoca.DesktopApp.Views.Dialogs;

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
            await viewModel.InitializeAsync();
        }
    }
}
