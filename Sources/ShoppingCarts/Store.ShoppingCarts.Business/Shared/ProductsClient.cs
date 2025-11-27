using Store.Products.Contracts;

namespace Store.ShoppingCarts.Business;

internal static class ProductsClient
{
    internal static async Task<ProductModel?> FindProductAsync(this ISender mediator, string id)
    {
        try
        {
            return await mediator.Send(new FindProductQuery(id));
        }
        catch (AppError)
        {
            return null;
        }
    }
}