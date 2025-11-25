using Store.Core.Domain.Repositories;
using Store.Orders.Domain;
using Store.Products.Domain;
using Store.ShoppingCarts.Domain;

namespace Store.ShoppingCarts.Business;

internal sealed class CheckoutCustomerCartCommandHandler(IShoppingCartsRepository shoppingCarts, RepositoriesContext repositories, ICurrentCustomer currentCustomer)
    : IRequestHandler<CheckoutCustomerCartCommand, IdModel>
{
    public async Task<IdModel> Handle(CheckoutCustomerCartCommand request, CancellationToken _)
    {
        var shoppingCartItems = await GetShoppingCartItems();

        shoppingCartItems.ForEach(l => l.Product.EnsureStockIsAvailable(l.CartLine.Quantity));

        var orderLines = shoppingCartItems
            .Select(item => OrderLine.Create(item.CartLine, item.Product))
            .ToList();

        var customerOrder = new Order(currentCustomer.Id, orderLines);
        await repositories.Orders.SaveOrderAsync(customerOrder);
        await shoppingCarts.DeleteAsync(currentCustomer.Id);

        await UpdateProductsStock(shoppingCartItems);

        return new IdModel(customerOrder.Id);
    }

    private async Task<List<(ShoppingCartLine CartLine, Products.Domain.Product Product)>> GetShoppingCartItems()
    {
        var shoppingCart = await shoppingCarts.FindOrEmptyAsync(currentCustomer.Id);

        shoppingCart.EnsureIsNotEmpty();

        return await shoppingCart.Lines
            .Where(cartLine => cartLine.Quantity > 0)
            .Select(async cartLine =>
            (
                cartLine,
                (await repositories.Products.FindAsync(cartLine.ProductId))!
            ))
            .ToListAsync();
    }

    private async Task UpdateProductsStock(List<(ShoppingCartLine CartLine, Products.Domain.Product Product)> items)
    {
        foreach (var item in items)
        {
            item.Product.DecreaseStock(item.CartLine.Quantity);
            await repositories.Products.UpdateAsync(item.Product);
        }
    }
}