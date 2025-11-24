using Store.Core.Domain.Repositories;

namespace Store.ShoppingCarts.Business;

internal sealed class ClearCustomerCartCommandHandler(IShoppingCartsRepository shoppingCarts, ICurrentCustomer currentCustomer)
    : IRequestHandler<ClearCustomerCartCommand>
{
    public Task Handle(ClearCustomerCartCommand request, CancellationToken _) 
        => shoppingCarts.DeleteAsync(currentCustomer.Id);
}