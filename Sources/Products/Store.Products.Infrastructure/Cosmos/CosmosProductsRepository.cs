using EnsureThat;
using Store.Products.Domain;
using Store.Shared.Infrastructure.Cosmos;

namespace Store.Products.Infrastructure.Cosmos;

internal sealed class CosmosProductsRepository(CosmosProductsDatabase db) : IProductsRepository
{
    public Task<IEnumerable<Product>> GetAsync(Func<Product, bool> filter)
    {
        var products = db.Products
            .GetItemLinqQueryable<Product>(true)
            .Where(filter)
            .AsEnumerable();

        return Task.FromResult(products);
    }

    public Task<Product?> FindAsync(string id)
        => db.Products.FindAsync<Product>(id, id.ToPartitionKey());

    public async Task AddAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        await db.Products.CreateItemAsync(product, product.Id.ToPartitionKey());
    }


    public async Task UpdateAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        await db.Products.ReplaceItemAsync(product, product.Id, product.Id.ToPartitionKey());
    }
}