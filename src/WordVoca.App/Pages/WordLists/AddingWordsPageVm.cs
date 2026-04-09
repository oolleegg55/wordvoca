using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WordVoca.App.Pages.WordLists;

public partial class AddingWordsPageVm : ObservableObject
{
    [ObservableProperty]
    private string _word = string.Empty;

    [ObservableProperty]
    private string _translation = string.Empty;

    [ObservableProperty]
    private string _note = string.Empty;

    [RelayCommand]
    private async Task AddWordsAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
