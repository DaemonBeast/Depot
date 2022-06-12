using Depot.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Depot.Storage.Local.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddLocalStorage(this IServiceCollection services)
    {
        services.AddSingleton<IStorage, LocalStorage>();

        return services;
    }
}