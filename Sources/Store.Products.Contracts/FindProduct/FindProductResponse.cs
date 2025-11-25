using Store.Shared;

namespace Store.Products.Contracts;

public sealed record FindProductResponse
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    // TODO: this should be extracted in something else
    public required PriceModel Price { get; init; }

    public required int Stock { get; init; }
}