using System.Threading.Tasks;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Services;

public interface IDialogService
{
    Task ShowModalAsync(ViewModelBase viewModelBase);
}
