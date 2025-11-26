using Store.Products.Contracts;
using Store.Shared;

namespace Store.Orders.Tests.Domain;

internal static class OrderProducts
{
    public static readonly ProductModel First = new()
    {
        Id = "761feed9-5db5-4526-98d7-526db42249b5",
        Name = "First",
        Price = PriceModel.Create(0.5m),
        Stock = 10
    };

    public static readonly ProductModel Second = new()
    {
        Id = "ce09d941-b2d1-42e8-a225-a6dc9d5edef9",
        Name = "Second",
        Price = PriceModel.Create(0.99m),
        Stock = 10
    };
}

