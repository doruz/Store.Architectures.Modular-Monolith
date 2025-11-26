using Store.Products.Contracts;
using Store.Products.Domain;

namespace Store.Products.Business;

public sealed record GetProductsQuery : IRequest<IEnumerable<ProductModel>>
{
    internal Func<Product, bool> Filter { get; }

    private GetProductsQuery(Func<Product, bool> filter) => Filter = filter;

    public static GetProductsQuery Available() => new(product => product is { Stock: > 0, DeletedAt: null });
    public static GetProductsQuery All() => new(product => product.DeletedAt == null);
}