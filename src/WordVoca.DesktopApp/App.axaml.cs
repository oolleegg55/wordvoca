using System;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.DependencyInjection;

using WordVoca.DesktopApp.Extensions;
using WordVoca.DesktopApp.ViewModels;
using WordVoca.DesktopApp.Views;
using WordVoca.Storage;

namespace WordVoca.DesktopApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ServiceCollection service = new();

        service.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
        service.AddSingleton(TimeProvider.System);

        service.AddRepositories();
        service.AddNavigation();
        service.AddViewModels();

        ServiceProvider serviceProvider = service.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
