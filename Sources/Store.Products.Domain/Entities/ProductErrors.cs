using Store.Shared;

namespace Store.Products.Domain;

public static class ProductErrors
{
    public static async Task<Product> EnsureExists(this Task<Product?> product, string productId)
        => EnsureExists(await product, productId);

    public static Product EnsureExists(this Product? product, string productId)
    {
        if(product.DoesNotExists())
        {
            throw NotFound(productId);
        }

        return product!;
    }

    public static bool DoesNotExists(this Product? product)
        => product is null || product.IsDeleted();

    private static AppError NotFound(string productId)
        => AppError.NotFound("product_not_found", productId);
}