using EnsureThat;
using Store.Products.Contracts;
using Store.ShoppingCarts.Domain;

namespace Store.ShoppingCarts.Business;

internal sealed class GetCustomerCartQueryHandler(IShoppingCartsRepository shoppingCarts, ISender mediator, ICurrentCustomer currentCustomer)
    : IRequestHandler<GetCustomerCartQuery, GetCustomerCartQueryResult>
{
    public async Task<GetCustomerCartQueryResult> Handle(GetCustomerCartQuery request, CancellationToken _)
    {
        var shoppingCart = await shoppingCarts.FindOrEmptyAsync(currentCustomer.Id);

        var cartLines = await shoppingCart.Lines
            .Select(async cartLine => new
            {
                CartLine = cartLine,
                Product = await mediator.FindProductAsync(cartLine.ProductId)
            })
            .ToListAsync();

        var lines = cartLines
            .Where(l => l.Product != null)
            .Select(l => ToShoppingCartLineModel(l.CartLine, l.Product!));

        return new GetCustomerCartQueryResult(lines);
    }

    private static GetCustomerCartLineModel ToShoppingCartLineModel(ShoppingCartLine cartLine, FindProductResponse product)
    {
        EnsureArg.IsNotNull(cartLine, nameof(cartLine));
        EnsureArg.IsNotNull(product, nameof(product));

        return new GetCustomerCartLineModel
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductPrice = product.Price,
            Quantity = cartLine.Quantity,
        };
    }
}