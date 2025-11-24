using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Orders.Domain;
using Store.Orders.Infrastructure.Cosmos;
using Store.Shared;
using System.Reflection;
using Store.Orders.Domain.Repositories;

namespace Store.Orders.Infrastructure;

public static class OrdersInfrastructureLayer
{
    public static Assembly Assembly => typeof(OrdersInfrastructureLayer).Assembly;

    public static IServiceCollection AddOrdersInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<CosmosOrdersOptions>(configuration.GetRequiredSection(CosmosOrdersOptions.Section).Bind)

            .AddSingleton<CosmosOrdersDatabase>()
            .AddSingleton<IAppInitializer, CosmosOrdersDatabase>()

            .AddRepository(configuration);
    }

    private static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        return configuration.UseCosmos()
            ? services.AddSingleton<IOrdersRepository, CosmosOrdersRepository>()
            : services.AddSingleton<IOrdersRepository, InMemoryOrdersRepository>();
    }

    private static bool UseCosmos(this IConfiguration configuration)
        => configuration["Orders:Persistence"].IsEqualTo("cosmos");
}