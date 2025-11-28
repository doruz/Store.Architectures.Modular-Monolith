using EnsureThat;
using Store.Shared;

namespace Store.ShoppingCarts.Domain;

public sealed class InMemoryShoppingCartsRepository : IShoppingCartsRepository
{
    private readonly List<ShoppingCart> _shoppingCarts = [];

    public Task<ShoppingCart?> FindAsync(string id)
        => Task.FromResult(_shoppingCarts.Find(cart => cart.Id.IsEqualTo(id)));

    public Task AddAsync(ShoppingCart cart)
    {
        _shoppingCarts.Add(EnsureArg.IsNotNull(cart, nameof(cart)));

        return Task.CompletedTask;
    }

    public async Task AddOrUpdateAsync(ShoppingCart cart)
    {
        await DeleteAsync(cart.Id);
        await AddAsync(cart);
    }

    public Task DeleteAsync(string id)
    {
        _shoppingCarts.RemoveAll(cart => cart.Id.IsEqualTo(id));

        return Task.CompletedTask;
    }
}