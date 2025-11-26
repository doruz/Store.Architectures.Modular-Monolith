using MediatR;

namespace Store.Orders.Contracts;

public sealed record OrderCreatedEvent(string CustomerId, string OrderId) : INotification;
