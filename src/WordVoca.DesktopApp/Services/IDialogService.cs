using System.Threading.Tasks;

using Avalonia.Controls;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Services;

public interface IDialogService
{
    Task ShowModalAsync<TView, TViewModel>()
        where TView : Window, new()
        where TViewModel : DialogViewModel;
}
