using System;
using System.Threading.Tasks;

using WordVoca.DesktopApp.ViewModels;

namespace WordVoca.DesktopApp.Services;

public interface IDialogService
{
    Task ShowModalAsync<TViewModel>(
        ViewModelBase? parent = null,
        Action<TViewModel>? afterCreation = null)
        where TViewModel : DialogViewModel;

    Task<TResult> ShowModalAsync<TViewModel, TResult>(
        ViewModelBase? parent = null,
        Action<TViewModel>? afterCreation = null)
        where TViewModel : DialogViewModel<TResult>;
}
