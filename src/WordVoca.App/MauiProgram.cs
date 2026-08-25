using CommunityToolkit.Maui;

using MauiIcons.Fluent;

using Microsoft.Extensions.Logging;

using WordVoca.App.Pages.Exercises;
using WordVoca.App.Pages.Main;
using WordVoca.App.Pages.WordLists;
using WordVoca.Core.Storages;
using WordVoca.Storage;
using WordVoca.Storage.Storages;

namespace WordVoca.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseFluentMauiIcons()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        StorageSettings storageSettings = new StorageSettings(Path.Combine(FileSystem.AppDataDirectory, "Data"));

        builder.Services.AddSingleton(storageSettings);
        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddSingleton<IWordListRepository, JsonWordListRepository>();

        builder.Services.AddTransient<MainPageVm>();
        builder.Services.AddTransient<CreationPageVm>();
        builder.Services.AddTransient<WordListPageVm>();
        builder.Services.AddTransient<AddingWordsPageVm>();
        builder.Services.AddTransient<WordCardsExerciseVm>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<CreationPage>();
        builder.Services.AddTransient<WordListPage>();
        builder.Services.AddTransient<AddingWordsPage>();
        builder.Services.AddTransient<WordCardsExerciseView>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
