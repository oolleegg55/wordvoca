using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Storages;

namespace WordVoca.DesktopApp.ViewModels;

public partial class CreationWordListViewModel : ViewModelBase
{
    private readonly IWordListStorage _wordListStorage;

    public CreationWordListViewModel(IWordListStorage wordListStorage)
    {
        _wordListStorage = wordListStorage;
    }


    [RelayCommand]
    private void Cancel()
    {
        OnCloseCallback();
    }

    [RelayCommand]
    private async Task AddAsync()
    {
        await Task.Delay(1000);
        OnCloseCallback();
    }
}
