using Store.Products.Contracts;
using Store.Products.Domain;

namespace Store.Products.Business;

internal static class ProductModelFactory
{
    internal static ProductModel Create(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        Stock = product.Stock
    };
}