global using Store.Shared;

using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Domain.Repositories;
using Store.Infrastructure.Persistence.InMemory;

namespace Store.Infrastructure.Persistence;

public static class PersistenceLayer
{
    public static Assembly Assembly => typeof(PersistenceLayer).Assembly;

    // TODO: top be removed
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //if (configuration.UseCosmos())
        //{
        //    services.AddCosmosPersistence(configuration);
        //}
        //else
        //{
        //    services.AddInMemoryPersistence();
        //}

        return services.AddSingleton<RepositoriesContext>();
    }

    internal static bool UseCosmos(this IConfiguration configuration)
        => configuration["AllowedPersistence"].IsEqualTo("cosmos");
}