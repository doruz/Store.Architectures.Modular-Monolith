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

        .AddRepository(configuration);
    }

    private static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        return configuration.UseCosmos() 
            ? services.AddSingleton<IProductsRepository, CosmosProductsRepository>() 
            : services.AddSingleton<IProductsRepository, InMemoryProductsRepository>();
    }

    private static bool UseCosmos(this IConfiguration configuration)
        => configuration["Products:Persistence"].IsEqualTo("cosmos");
}