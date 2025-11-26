using Store.Orders.Contracts;

namespace Store.Products.Business;

internal sealed class UpdateProductStocksHandler : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        // TODO: to be implemented
        return Task.CompletedTask;
    }
}