using CommunityToolkit.Mvvm.Input;

namespace WordVoca.DesktopApp.ViewModels.Dialogs;

public partial class ConfirmationViewModel : DialogViewModel<bool>
{
    [RelayCommand]
    private void Ok()
    {
        Close(true);
    }

    [RelayCommand]
    private void Cancel()
    {
        Close(false);
    }
}
