using Store.ShoppingCarts.Domain;

namespace Store.ShoppingCarts.Business;

internal sealed class UpdateCustomerCartCommandHandler(IShoppingCartsRepository shoppingCarts, ICurrentCustomer currentCustomer)
    : IRequestHandler<UpdateCustomerCartCommand>
{
    public async Task Handle(UpdateCustomerCartCommand request, CancellationToken _)
    {
        if (request.Lines.IsEmpty())
        {
            return;
        }

        var shoppingCart = await shoppingCarts.FindOrEmptyAsync(currentCustomer.Id);

        shoppingCart.UpdateOrRemoveLines(request.Lines.Select(ToShoppingCartLine));

        await shoppingCarts.AddOrUpdateAsync(shoppingCart);
    }

    private static ShoppingCartLine ToShoppingCartLine(UpdateCustomerCartLineModel cartLine)
        => new(cartLine.ProductId, cartLine.Quantity);
}