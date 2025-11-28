using Store.Shared.Infrastructure.Cosmos;

namespace Store.ShoppingCarts.Infrastructure.Cosmos;

internal sealed record CosmosShoppingCartsOptions : CosmosOptions
{
    internal const string Section = "ShoppingCarts:CosmosOptions";
}