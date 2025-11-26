using Store.Orders.Contracts;
using Store.ShoppingCarts.Domain;

namespace Store.ShoppingCarts.Business;

internal sealed class ClearCustomerCartCommandHandler(IShoppingCartsRepository shoppingCarts, ICurrentCustomer currentCustomer) :
    IRequestHandler<ClearCustomerCartCommand>,
    INotificationHandler<NewOrderEvent>
{
    public Task Handle(ClearCustomerCartCommand request, CancellationToken _) 
        => shoppingCarts.DeleteAsync(currentCustomer.Id);

    public Task Handle(NewOrderEvent request, CancellationToken _)
        => shoppingCarts.DeleteAsync(request.CustomerId);
}