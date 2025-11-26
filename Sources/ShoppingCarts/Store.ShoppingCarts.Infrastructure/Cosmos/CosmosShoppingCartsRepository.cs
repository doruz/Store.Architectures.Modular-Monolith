using EnsureThat;
using Store.Shared.Infrastructure.Cosmos;
using Store.ShoppingCarts.Domain;

namespace Store.ShoppingCarts.Infrastructure.Cosmos;

internal sealed class CosmosShoppingCartsRepository(CosmosShoppingCartsDatabase db) : IShoppingCartsRepository
{
    public Task<ShoppingCart?> FindAsync(string id)
        => db.ShoppingCarts.FindAsync<ShoppingCart>(id, id.ToPartitionKey());

    public async Task AddOrUpdateAsync(ShoppingCart cart)
    {
        EnsureArg.IsNotNull(cart, nameof(cart));

        await db.ShoppingCarts.UpsertItemAsync(cart);
    }

    public Task DeleteAsync(string id)
        => db.ShoppingCarts.DeleteAsync<ShoppingCart>(id, id.ToPartitionKey());
}