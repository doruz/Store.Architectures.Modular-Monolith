global using MediatR;
global using Store.Shared;

using System.Reflection;

namespace Store.ShoppingCarts.Business;

public static class ShoppingCartsBusinessLayer
{
    public static Assembly Assembly => typeof(ShoppingCartsBusinessLayer).Assembly;
}