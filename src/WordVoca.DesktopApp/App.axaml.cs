using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Microsoft.Extensions.DependencyInjection;

using WordVoca.Core.Storages;
using WordVoca.DesktopApp.Services;
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
        service.AddScoped<MainWindowViewModel>();
        service.AddScoped<CreationWordListViewModel>();
        service.AddScoped<IWordListStorage, JsonWordListStorage>();
        service.AddScoped<IDialogService, DialogService>();

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
