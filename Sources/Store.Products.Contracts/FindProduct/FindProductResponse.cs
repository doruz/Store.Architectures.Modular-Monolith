using Store.Shared;

namespace Store.Products.Contracts;

// TODO: maybe to rename it to ProductModel
public sealed record FindProductResponse
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    // TODO: this should be extracted in something else
    public required PriceModel Price { get; init; }

    public required int Stock { get; init; }

    internal bool StockIsAvailable(int quantity) => quantity.IsInRange(0, Stock);
}

// TODO: this maybe should be moved in ShoppingCart
public static class FindProductErrors
{
    public static FindProductResponse EnsureStockIsAvailable(this FindProductResponse product, int quantity) =>
        product.StockIsAvailable(quantity)
            ? product
            : throw AppError.Conflict("product_stock_not_available", product.Id);
}