using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Store.Shared.Infrastructure.Cosmos;

namespace Store.ShoppingCarts.Infrastructure.Cosmos;

internal sealed class CosmosShoppingCartsDatabase(CosmosClient cosmosClient, IOptions<CosmosShoppingCartsOptions> options)
    : CosmosDatabase(cosmosClient, options)
{
    private const string ShoppingCartsContainer = "ShoppingCarts";

    protected override IReadOnlyList<ContainerProperties> Containers { get; } =
    [
        new ContainerProperties(ShoppingCartsContainer, "/id")
    ];

    public Container ShoppingCarts => GetContainer(ShoppingCartsContainer);
}