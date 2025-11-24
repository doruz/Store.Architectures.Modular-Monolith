using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosDatabaseContainers(CosmosClient cosmosClient, IOptions<CosmosOptions> options)
{
    public const string ProductsName = "Products";

    public Container Products => GetContainer(ProductsName);

    private Container GetContainer(string containerName)
        => cosmosClient.GetContainer(options.Value.DatabaseName, containerName);
}