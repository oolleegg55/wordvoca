using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WordVoca.DesktopApp.ViewModels;

public partial class CreationWordListViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isVisible = false;

    [RelayCommand]
    private void Cancel()
    {
        IsVisible = false;
    }
}
