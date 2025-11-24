using Store.Orders.Domain;
using Store.Products.Domain;
using Store.ShoppingCarts.Domain;

namespace Store.Infrastructure.Persistence.InMemory
{
    internal sealed class InMemoryDatabase
    {
        public List<Product> Products { get; } = [];

        public List<ShoppingCart> ShoppingCarts { get; } = [];

        public List<Order> Orders { get; } = [];
    }
}
