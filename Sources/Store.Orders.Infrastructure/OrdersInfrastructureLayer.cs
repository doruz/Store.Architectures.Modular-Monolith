using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

using Store.Shared;
using Store.Orders.Domain;
using Store.Orders.Infrastructure.Cosmos;

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

            .AddSingleton<IOrdersRepository, CosmosOrdersRepository>();
    }
}