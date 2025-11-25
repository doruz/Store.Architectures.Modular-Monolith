using Store.Products.Contracts;

namespace Store.ShoppingCarts.Business;

internal static class ProductsClient
{
    public static async Task<FindProductResponse?> FindProductAsync(this ISender mediator, string id) 
        => await mediator.Send(new FindProductRequest(id));
}