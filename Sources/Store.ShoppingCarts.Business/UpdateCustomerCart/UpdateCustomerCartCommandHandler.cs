using Store.Products.Contracts;
using Store.ShoppingCarts.Domain;

namespace Store.ShoppingCarts.Business;

internal sealed class UpdateCustomerCartCommandHandler(IShoppingCartsRepository shoppingCarts, ISender mediator, ICurrentCustomer currentCustomer)
    : IRequestHandler<UpdateCustomerCartCommand>
{
    public async Task Handle(UpdateCustomerCartCommand request, CancellationToken _)
    {
        if (request.Lines.IsEmpty())
        {
            return;
        }

        var shoppingCart = await shoppingCarts.FindOrEmptyAsync(currentCustomer.Id);

        shoppingCart.UpdateOrRemoveLines(await GetValidLines(request.Lines));

        await shoppingCarts.AddOrUpdateAsync(shoppingCart);
    }

    private async Task<ShoppingCartLine[]> GetValidLines(IEnumerable<UpdateCustomerCartLineModel> cartLines)
    {
        var lines = await cartLines
            .Select(async cartLine => new
            {
                CartLine = cartLine,
                Product = await mediator.FindProductAsync(cartLine.ProductId)
            })
            .ToListAsync(); 
        
        // TODO: this validation could be done only at checkoout
        lines.ForEach(l =>
        {
            l.Product
                .EnsureIsNotNull(l.CartLine.ProductId)
                .EnsureStockIsAvailable(l.CartLine.Quantity);
        });

        return lines
            .Select(l => ToShoppingCartLine(l.CartLine))
            .ToArray();
    }

    private static ShoppingCartLine ToShoppingCartLine(UpdateCustomerCartLineModel cartLine)
        => new(cartLine.ProductId, cartLine.Quantity);
}