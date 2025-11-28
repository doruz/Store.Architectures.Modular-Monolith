using Store.Shared.Infrastructure.Cosmos;

namespace Store.Orders.Infrastructure.Cosmos;

internal sealed record CosmosOrdersOptions : CosmosOptions
{
    internal const string Section = "Orders:CosmosOptions";
}