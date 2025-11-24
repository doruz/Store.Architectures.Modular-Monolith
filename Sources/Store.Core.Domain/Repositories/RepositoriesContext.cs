using Store.Products.Domain;

namespace Store.Core.Domain.Repositories;

// TODO: this will be removed at the end of the migration
public sealed class RepositoriesContext(
    IProductsRepository products,
    IShoppingCartsRepository shoppingCarts,
    IOrdersRepository orders)
{
    public IProductsRepository Products { get; } = products;

    public IShoppingCartsRepository ShoppingCarts { get; } = shoppingCarts;

    public IOrdersRepository Orders { get; } = orders;
}