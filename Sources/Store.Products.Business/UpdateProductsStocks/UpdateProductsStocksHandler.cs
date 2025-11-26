using Store.Orders.Contracts;
using Store.Products.Domain;

namespace Store.Products.Business;

internal sealed class UpdateProductsStocksHandler(IProductsRepository products) 
    : INotificationHandler<NewOrderEvent>
{
    public async Task Handle(NewOrderEvent newOrder, CancellationToken _)
    {
        foreach (var orderedProduct in newOrder.Products)
        {
            var product = await products.FindAsync(orderedProduct.Id);

            product!.DecreaseStock(orderedProduct.Quantity);

            await products.UpdateAsync(product);
        }
    }
}