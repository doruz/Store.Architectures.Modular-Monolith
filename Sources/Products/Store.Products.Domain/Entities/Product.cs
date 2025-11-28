using EnsureThat;
using Store.Shared;

namespace Store.Products.Domain;

public sealed class Product(string name, Price price, int stock) : Entity
{
    public string Name { get; private set; } = EnsureArg.IsNotNullOrEmpty(name);

    public Price Price { get; private set; } = EnsureArg.IsNotNull(price);

    public int Stock { get; private set; } = stock;

    public void Update(string? name, decimal? price, int? stock)
    {
        Name = EnsureArg.IsNotNullOrEmpty(name ?? Name);
        Price = EnsureArg.IsNotNull(price ?? Price);
        Stock = stock ?? Stock;
    }

    public void DecreaseStock(int quantity) => Stock -= quantity;
}