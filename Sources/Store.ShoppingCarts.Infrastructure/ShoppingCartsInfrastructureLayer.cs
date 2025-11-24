using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

using Store.Shared;
using Store.ShoppingCarts.Domain;
using Store.ShoppingCarts.Domain.Repositories;
using Store.ShoppingCarts.Infrastructure.Cosmos;

namespace Store.ShoppingCarts.Infrastructure;

public static class ShoppingCartsInfrastructureLayer
{
    public static Assembly Assembly => typeof(ShoppingCartsInfrastructureLayer).Assembly;

    public static IServiceCollection AddShoppingCartsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.UseCosmos())
        {
            return services
                .Configure<CosmosShoppingCartsOptions>(configuration.GetRequiredSection(CosmosShoppingCartsOptions.Section).Bind)

                .AddSingleton<CosmosShoppingCartsDatabase>()
                .AddSingleton<IAppInitializer, CosmosShoppingCartsDatabase>()
                .AddSingleton<IShoppingCartsRepository, CosmosShoppingCartsRepository>();
        }

        return services.AddSingleton<IShoppingCartsRepository, InMemoryShoppingCartsRepository>();
    }

    private static bool UseCosmos(this IConfiguration configuration)
        => configuration["ShoppingCarts:Persistence"].IsEqualTo("cosmos");
}
