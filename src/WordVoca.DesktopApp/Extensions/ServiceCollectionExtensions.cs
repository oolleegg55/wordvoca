using System;

using Avalonia.Controls;

using Microsoft.Extensions.DependencyInjection;

using WordVoca.DesktopApp.Services;
using WordVoca.DesktopApp.ViewModels;
using WordVoca.DesktopApp.ViewModels.Dialogs;
using WordVoca.DesktopApp.ViewModels.Pages;
using WordVoca.DesktopApp.Views.Dialogs;

namespace WordVoca.DesktopApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection service)
    {
        service.AddScoped<MainWindowViewModel>();

        service.AddTransient<MainViewModel>();
        service.AddTransient<WordListViewModel>();
        service.AddTransient<CreationWordListViewModel>();
        service.AddTransient<AddWordViewModel>();
        service.AddTransient<ConfirmationViewModel>();

        return service;
    }

    public static IServiceCollection AddNavigation(this IServiceCollection service)
    {
        service.AddSingleton<IDialogService, DialogService>();
        service.AddSingleton<PageFactory>();
        service.AddSingleton<DialogViewFactory>();

        service.AddSingleton<Func<Type, ViewModelBase>>(x => type => type switch
        {
            _ when type == typeof(MainViewModel) => x.GetRequiredService<MainViewModel>(),
            _ when type == typeof(WordListViewModel) => x.GetRequiredService<WordListViewModel>(),
            _ when type == typeof(CreationWordListViewModel) => x.GetRequiredService<CreationWordListViewModel>(),
            _ when type == typeof(AddWordViewModel) => x.GetRequiredService<AddWordViewModel>(),
            _ when type == typeof(ConfirmationViewModel) => x.GetRequiredService<ConfirmationViewModel>(),
            _ => throw new InvalidOperationException($"Page of type {type?.FullName} has no view model"),
        });

        service.AddSingleton<Func<Type, Window>>(x => type => type switch
        {
            _ when type == typeof(AddWordViewModel) => new AddWordView(),
            _ when type == typeof(CreationWordListViewModel) => new CreationWordListView(),
            _ when type == typeof(ConfirmationViewModel) => new ConfirmationView(),
            _ => throw new InvalidOperationException($"Window of type {type?.FullName} has no view model"),
        });

        return service;
    }
}
