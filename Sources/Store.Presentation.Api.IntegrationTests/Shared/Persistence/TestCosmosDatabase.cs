using EnsureThat;
using Store.Orders.Domain;
using Store.Orders.Infrastructure.Cosmos;
using Store.Products.Domain;
using Store.Products.Infrastructure.Cosmos;
using Store.Shared;
using Store.Shared.Infrastructure.Cosmos;

namespace Store.Presentation.Api.IntegrationTests;

internal sealed class TestCosmosDatabase(CosmosProductsDatabase productsDb, CosmosOrdersDatabase ordersDb)
{
    public async Task EnsureIsInitialized()
    {
        await DeleteTestProducts();
        await AddTestProducts();
    }

    public Task DeleteCustomerOrders(string customerId)
    {
        EnsureIsTestDatabase();

        return ordersDb.Orders.DeleteAllItemsByPartitionKeyStreamAsync(customerId.ToPartitionKey());
    }

    public Task<Order?> FindCustomerOrder(string customerId, string orderId)
    {
        return ordersDb.Orders.FindAsync<Order>(orderId, customerId.ToPartitionKey());
    }

    private async Task AddTestProducts()
    {
        EnsureIsTestDatabase();

        foreach (var product in TestProducts.All)
        {
            await productsDb.Products.UpsertItemAsync(product);
        }
    }

    private async Task DeleteTestProducts()
    {
        EnsureIsTestDatabase();

        await productsDb.Products
            .GetItemLinqQueryable<Product>(true)
            .AsEnumerable()
            .ForEachAsync(async p =>
            {
                await productsDb.Products.DeleteAsync<Product>(p.Id, p.Id.ToPartitionKey());
            });
    }

    private void EnsureIsTestDatabase()
        => EnsureArg.IsTrue(productsDb.Products.Database.Id.Contains("Tests"));
}