using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

using Store.Shared;
using Store.Products.Domain;
using Store.Products.Infrastructure.Cosmos;

namespace Store.Products.Infrastructure;

public static class ProductsInfrastructureLayer
{
    public static Assembly Assembly => typeof(ProductsInfrastructureLayer).Assembly;

    public static IServiceCollection AddProductsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.UseCosmos())
        {
            return services
                .Configure<CosmosProductsOptions>(configuration.GetRequiredSection(CosmosProductsOptions.Section).Bind)

                .AddSingleton<CosmosProductsDatabase>()
                .AddSingleton<IAppInitializer, CosmosProductsDatabase>()
                .AddSingleton<IProductsRepository, CosmosProductsRepository>();
        }

        return services.AddSingleton<IProductsRepository, InMemoryProductsRepository>();
    }

    private static bool UseCosmos(this IConfiguration configuration)
        => configuration["Products:Persistence"].IsEqualTo("cosmos");
}