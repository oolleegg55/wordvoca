using System;

using Microsoft.Extensions.DependencyInjection;

using WordVoca.DesktopApp.Services;
using WordVoca.DesktopApp.ViewModels;
using WordVoca.DesktopApp.ViewModels.Dialogs;
using WordVoca.DesktopApp.ViewModels.Pages;

namespace WordVoca.DesktopApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection service)
    {
        service.AddScoped<MainWindowViewModel>();

        service.AddTransient<MainViewModel>();
        service.AddTransient<WordListViewModel>();
        service.AddTransient<CreationWordListViewModel>();

        return service;
    }

    public static IServiceCollection AddNavigation(this IServiceCollection service)
    {
        service.AddSingleton<IDialogService, DialogService>();
        service.AddSingleton<PageFactory>();

        service.AddSingleton<Func<Type, ViewModelBase>>(x => type => type switch
        {
            _ when type == typeof(MainViewModel) => x.GetRequiredService<MainViewModel>(),
            _ when type == typeof(WordListViewModel) => x.GetRequiredService<WordListViewModel>(),
            _ when type == typeof(CreationWordListViewModel) => x.GetRequiredService<CreationWordListViewModel>(),
            _ => throw new InvalidOperationException($"Page of type {type?.FullName} has no view model"),
        });

        return service;
    }
}
