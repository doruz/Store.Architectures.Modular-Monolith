using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Store.Shared.Infrastructure.Cosmos;

namespace Store.ShoppingCarts.Infrastructure.Cosmos;

internal sealed class CosmosShoppingCartsDatabase(CosmosClient cosmosClient, IOptions<CosmosShoppingCartsOptions> options)
    : CosmosDatabase(cosmosClient, options)
{
    private const string CustomerShoppingCarts = "CustomerShoppingCarts";

    protected override IReadOnlyList<ContainerProperties> Containers { get; } =
    [
        new ContainerProperties(CustomerShoppingCarts, "/id")
    ];

    public Container ShoppingCarts => GetContainer(CustomerShoppingCarts);
}