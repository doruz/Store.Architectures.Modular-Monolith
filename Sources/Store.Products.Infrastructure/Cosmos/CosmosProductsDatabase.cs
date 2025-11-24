using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Store.Shared.Infrastructure.Cosmos;

namespace Store.Products.Infrastructure.Cosmos;

internal sealed class CosmosProductsDatabase(CosmosClient cosmosClient, IOptions<CosmosProductsOptions> options)
    : CosmosDatabase(cosmosClient, options)
{
    private const string ProductsContainer = "Products";

    protected override IReadOnlyList<ContainerProperties> Containers { get; } =
    [
        new ContainerProperties(ProductsContainer, "/id")
    ];

    public Container Products => GetContainer(ProductsContainer);
}