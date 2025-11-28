using Store.Orders.Business;
using Store.Orders.Domain;
using Store.Orders.Infrastructure;
using Store.Products.Business;
using Store.Products.Domain;
using Store.Products.Infrastructure;
using Store.ShoppingCarts.Business;
using Store.ShoppingCarts.Infrastructure;

namespace Store.Tests;

public static class ModuleTypes
{
    public static class Orders
    {
        public static Types Business => Types.InAssembly(OrdersBusinessLayer.Assembly);
        public static Types Domain => Types.InAssembly(OrdersDomainLayer.Assembly);
        public static Types Infrastructure => Types.InAssembly(OrdersInfrastructureLayer.Assembly);
    }

    public static class Products
    {
        public static Types Business => Types.InAssembly(ProductsBusinessLayer.Assembly);
        public static Types Domain => Types.InAssembly(ProductsDomainLayer.Assembly);
        public static Types Infrastructure => Types.InAssembly(ProductsInfrastructureLayer.Assembly);
    }

    public static class ShoppingCarts
    {
        public static Types Business => Types.InAssembly(ShoppingCartsBusinessLayer.Assembly);
        public static Types Domain => Types.InAssembly(ShoppingCartsBusinessLayer.Assembly);
        public static Types Infrastructure => Types.InAssembly(ShoppingCartsInfrastructureLayer.Assembly);
    }
}