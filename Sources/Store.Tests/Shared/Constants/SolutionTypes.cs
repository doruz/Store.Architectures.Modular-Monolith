using Store.Orders.Business;
using Store.Orders.Domain;
using Store.Orders.Infrastructure;
using Store.Products.Business;
using Store.Products.Domain;
using Store.Products.Infrastructure;
using Store.ShoppingCarts.Business;
using Store.ShoppingCarts.Domain;
using Store.ShoppingCarts.Infrastructure;

namespace Store.Tests;

internal static class SolutionTypes
{
    public static Types Domain => Types.InAssemblies(
    [
        OrdersDomainLayer.Assembly,
        ProductsDomainLayer.Assembly,
        ShoppingCartsDomainLayer.Assembly
    ]);

    public static Types Business => Types.InAssemblies(
    [
        OrdersBusinessLayer.Assembly,
        ProductsBusinessLayer.Assembly,
        ShoppingCartsBusinessLayer.Assembly
    ]);

    public static Types Infrastructure => Types.InAssemblies(
    [
        OrdersInfrastructureLayer.Assembly,
        ProductsInfrastructureLayer.Assembly,
        ShoppingCartsInfrastructureLayer.Assembly
    ]);

    public static Types Api => Types.InAssembly(ApiLayer.Assembly);

    public static Types All => Types.FromPath(Directory.GetCurrentDirectory());

    // TODO: OLD TYPES

    public static class Core
    {
        public static Types Shared => null;// Types.InAssembly(SharedLayer.Assembly);
        public static Types Domain => null; // Types.InAssembly(DomainLayer.Assembly);
        public static Types Business => null; // Types.InAssembly(BusinessLayer.Assembly);
    }
}