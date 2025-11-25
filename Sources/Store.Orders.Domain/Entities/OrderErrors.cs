using Store.Shared;

namespace Store.Orders.Domain;

public static class OrderErrors
{
    public static Order EnsureIsNotEmpty(this Order? order)
    {
        if (order is null || order.Lines.IsEmpty())
        {
            throw AppError.NotFound("order_is_empty");
        }

        return order;
    }
}