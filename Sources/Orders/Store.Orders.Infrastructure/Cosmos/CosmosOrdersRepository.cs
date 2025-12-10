using EnsureThat;
using Microsoft.Azure.Cosmos;
using Store.Orders.Domain;
using Store.Shared.Infrastructure.Cosmos;

namespace Store.Orders.Infrastructure.Cosmos;

internal sealed class CosmosOrdersRepository(CosmosOrdersDatabase db) : IOrdersRepository
{
    public Task<IEnumerable<Order>> GetAllAsync(string customerId)
    {
        EnsureArg.IsNotNullOrEmpty(customerId, nameof(customerId));

        var requestOptions = new QueryRequestOptions
        {
            PartitionKey = customerId.ToPartitionKey()
        };

        var orders = db.Orders
            .GetItemLinqQueryable<Order>(true, requestOptions: requestOptions)
            .AsEnumerable();

        return Task.FromResult(orders);
    }

    public Task<Order?> FindAsync(string customerId, string id)
        => db.Orders.FindAsync<Order>(id, customerId.ToPartitionKey());

    public async Task SaveAsync(Order order)
    {
        EnsureArg.IsNotNull(order, nameof(order));

        await db.Orders.CreateItemAsync(order);
    }
}