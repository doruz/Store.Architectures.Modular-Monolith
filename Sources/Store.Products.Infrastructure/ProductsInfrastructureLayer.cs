using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Store.Products.Domain;
using Store.Products.Infrastructure.Cosmos;
using Store.Shared;

namespace Store.Products.Infrastructure;

public static class ProductsInfrastructureLayer
{
    public static Assembly Assembly => typeof(ProductsInfrastructureLayer).Assembly;

    public static IServiceCollection AddProductsInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<CosmosProductsOptions>(configuration.GetRequiredSection(CosmosProductsOptions.Section).Bind)

        .AddSingleton<CosmosProductsDatabase>()
        .AddSingleton<IAppInitializer, CosmosProductsDatabase>()

        .AddSingleton<IProductsRepository, CosmosProductsRepository>();
    }
}