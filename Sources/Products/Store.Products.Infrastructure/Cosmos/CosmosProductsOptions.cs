using Store.Shared.Infrastructure.Cosmos;

namespace Store.Products.Infrastructure.Cosmos;

internal sealed record CosmosProductsOptions : CosmosOptions
{
    internal const string Section = "Products:CosmosOptions";
}