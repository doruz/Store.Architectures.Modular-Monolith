using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared.Infrastructure.Cosmos;

namespace Store.Shared.Infrastructure;

public static class SharedInfrastructureLayer
{
    public static Assembly Assembly => typeof(SharedInfrastructureLayer).Assembly;

    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton(CosmosClientFactory.Create(configuration));
    }
}