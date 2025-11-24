using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Store.Shared.Infrastructure.Cosmos;

namespace Store.Orders.Infrastructure.Cosmos;

internal sealed class CosmosOrdersDatabase(CosmosClient cosmosClient, IOptions<CosmosOrdersOptions> options)
    : CosmosDatabase(cosmosClient, options)
{
    private const string CustomerOrders = "CustomerOrders";

    protected override IReadOnlyList<ContainerProperties> Containers { get; } =
    [
        new ContainerProperties(CustomerOrders, "/customerId")
    ];

    public Container Orders => GetContainer(CustomerOrders);
}