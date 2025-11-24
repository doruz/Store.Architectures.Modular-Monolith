using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;
using Store.Shared;
using AppErrors = Store.Shared.AppErrors;

namespace Store.Core.Business.Orders;

internal sealed class FindCustomerOrderQueryHandler(RepositoriesContext repositories, ICurrentCustomer currentCustomer)
    : IRequestHandler<FindCustomerOrderQuery, FindCustomerOrderQueryResult>
{
    public async Task<FindCustomerOrderQueryResult> Handle(FindCustomerOrderQuery request, CancellationToken _)
    {
        var order = await AppErrors.EnsureIsNotNull(repositories.Orders
                .FindOrderAsync(currentCustomer.Id, request.OrderId), request.OrderId);

        return order.Map(ToOrderDetailedModel);
    }

    private static FindCustomerOrderQueryResult ToOrderDetailedModel(Order order) => new()
    {
        Id = order.Id,
        OrderedAt = order.CreatedAt.ToDateTimeModel(),

        TotalProducts = order.TotalProducts,
        TotalPrice = PriceModel.Create(order.TotalPrice),

        Lines = order.Lines.Select(ToOrderLineModel).ToList()
    };

    private static OrderDetailedLineModel ToOrderLineModel(OrderLine product) => new()
    {
        ProductId = product.ProductId,
        ProductName = product.ProductName,
        ProductPrice = PriceModel.Create(product.ProductPrice),
        Quantity = product.Quantity,
        TotalPrice = PriceModel.Create(product.TotalPrice)
    };
}