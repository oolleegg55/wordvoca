using Microsoft.Extensions.DependencyInjection;

using WordVoca.Core.Repositories;
using WordVoca.Storage.Storages;

namespace WordVoca.Storage;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection service)
    {
        RepositorySettings storageSettings = new(Path.Combine(AppContext.BaseDirectory, "WordLists"));

        service.AddSingleton((sp) =>
        {
            return storageSettings;
        });

        service.AddSingleton<IWordListRepository, JsonWordListRepository>();
        
        return service;
    }
}
