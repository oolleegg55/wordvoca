using MauiIcons.Fluent;

using Microsoft.Extensions.Logging;

using WordVoca.App.Pages.Main;

namespace WordVoca.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseFluentMauiIcons()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<MainPageVm>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
