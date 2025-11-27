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

    //public static class Domain
    //{
    //    public static Types Orders => Types.InAssembly(OrdersDomainLayer.Assembly);
    //    public static Types Domain => Types.InAssembly(DomainLayer.Assembly);
    //    public static Types Business => Types.InAssembly(BusinessLayer.Assembly);
    //}

    public static class Core
    {
        public static Types All => Types.InAssemblies(
        [
            //SharedLayer.Assembly,
            //DomainLayer.Assembly,
            //BusinessLayer.Assembly
        ]);

        public static Types Shared => null;// Types.InAssembly(SharedLayer.Assembly);
        public static Types Domain => null; // Types.InAssembly(DomainLayer.Assembly);
        public static Types Business => null; // Types.InAssembly(BusinessLayer.Assembly);
    }

    //public static class Infrastructure
    //{
    //    public static Types All => Types.InAssemblies(
    //    [
    //        //PersistenceLayer.Assembly,
    //    ]);

    //    public static Types Persistence => null; //Types.InAssembly(PersistenceLayer.Assembly);
    //}

    public static class Presentation
    {
        public static Types All => Types.InAssemblies(
        [
            ApiLayer.Assembly,
        ]);

        public static Types Api => Types.InAssembly(ApiLayer.Assembly);
    }
}