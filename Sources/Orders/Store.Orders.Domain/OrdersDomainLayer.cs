using System.Reflection;

namespace Store.Orders.Domain;

public static class OrdersDomainLayer
{
    public static Assembly Assembly => typeof(OrdersDomainLayer).Assembly;
}