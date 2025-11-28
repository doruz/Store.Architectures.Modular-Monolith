global using MediatR;
global using Store.Shared;

using System.Reflection;

namespace Store.Orders.Business;

public static class OrdersBusinessLayer
{
    public static Assembly Assembly => typeof(OrdersBusinessLayer).Assembly;
}