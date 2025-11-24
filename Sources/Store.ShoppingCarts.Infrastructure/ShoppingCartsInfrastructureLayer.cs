using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared;
using System.Reflection;
using Store.ShoppingCarts.Domain;
using Store.ShoppingCarts.Infrastructure.Cosmos;

namespace Store.ShoppingCarts.Infrastructure;

public static class ShoppingCartsInfrastructureLayer
{
    public static Assembly Assembly => typeof(ShoppingCartsInfrastructureLayer).Assembly;

    public static IServiceCollection AddShoppingCartsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<CosmosShoppingCartsOptions>(configuration.GetRequiredSection(CosmosShoppingCartsOptions.Section).Bind)

            .AddSingleton<CosmosShoppingCartsDatabase>()
            .AddSingleton<IAppInitializer, CosmosShoppingCartsDatabase>()

            .AddSingleton<IShoppingCartsRepository, CosmosShoppingCartsRepository>();
    }
}
