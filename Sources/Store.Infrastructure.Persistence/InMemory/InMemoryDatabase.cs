using Store.Core.Domain.Entities;
using Store.Products.Domain;

namespace Store.Infrastructure.Persistence.InMemory
{
    internal sealed class InMemoryDatabase
    {
        public List<Product> Products { get; } = [];

        public List<ShoppingCart> ShoppingCarts { get; } = [];

        public List<Order> Orders { get; } = [];
    }
}
