using Store.Shared;

namespace Store.Products.Contracts;

public static class ProductModelErrors
{
    public static ProductModel EnsureStockIsAvailable(this ProductModel product, int quantity) =>
        product.IsStockAvailable(quantity)
            ? product
            : throw AppError.Conflict("product_stock_is_not_available", product.Id);
}