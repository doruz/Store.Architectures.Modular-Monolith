using System.Reflection;
using System.Text.Json;
using Store.Core.Business;
using Store.Orders.Business;
using Store.Orders.Infrastructure;
using Store.Products.Business;
using Store.Products.Infrastructure;
using Store.Shared;
using Store.Shared.Infrastructure;
using Store.ShoppingCarts.Business;
using Store.ShoppingCarts.Infrastructure;

public static class ApiLayer
{
    public static Assembly Assembly => typeof(ApiLayer).Assembly;

    public static IServiceCollection AddCurrentSolution(this IServiceCollection services, IConfiguration configuration)
    {
        return services

            .AddMediator()

            .AddSharedInfrastructure(configuration)
            .AddOrdersInfrastructure(configuration)
            .AddShoppingCartsInfrastructure(configuration)
            .AddProductsInfrastructure(configuration)

            .AddTransient<ICurrentCustomer, CurrentCustomer>()
            .AddHostedService<AppInitializationService>();
    }

    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        return services.AddOpenApi(options =>
        {
            options.AddOperationTransformer((operation, _, _) =>
            {
                foreach (var parameter in operation.Parameters ?? [])
                {
                    parameter.Name = JsonNamingPolicy.CamelCase.ConvertName(parameter.Name);
                }

                return Task.CompletedTask;
            });
        });
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            ProductsBusinessLayer.Assembly,
            ShoppingCartsBusinessLayer.Assembly,
            OrdersBusinessLayer.Assembly
        };

        return services.AddMediatR(config => config.RegisterServicesFromAssemblies(assemblies));
    }
}