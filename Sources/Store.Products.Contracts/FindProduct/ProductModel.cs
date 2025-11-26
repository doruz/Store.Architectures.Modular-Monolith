using Store.Shared;

namespace Store.Products.Contracts;

public sealed record ProductModel
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    // TODO: this should be extracted in something else
    public required PriceModel Price { get; init; }

    public required int Stock { get; init; }

    internal bool StockIsAvailable(int quantity) => quantity.IsInRange(0, Stock);
}

public static class FindProductErrors
{
    public static ProductModel EnsureExists(this ProductModel? product, string productId)
    {
        if (product is null)
        {
            throw AppError.NotFound("product_not_found", productId);
        }

        return product;
    }

    public static ProductModel EnsureStockIsAvailable(this ProductModel product, int quantity) =>
        product.StockIsAvailable(quantity)
            ? product
            : throw AppError.Conflict("product_stock_not_available", product.Id);
}