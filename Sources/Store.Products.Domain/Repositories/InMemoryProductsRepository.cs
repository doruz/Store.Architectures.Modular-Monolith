using EnsureThat;
using Store.Shared;

namespace Store.Products.Domain;

public sealed class InMemoryProductsRepository : IProductsRepository
{
    private readonly List<Product> _products = [];

    public Task<IEnumerable<Product>> GetAsync(Func<Product, bool> filter)
        => Task.FromResult(_products.Where(filter));

    public Task<Product?> FindAsync(string id)
        => Task.FromResult(_products.Find(p => p.Id.IsEqualTo(id)));

    public Task AddAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        throw new Exception("");

        _products.Add(product);

        return Task.CompletedTask;
    }

    public async Task UpdateAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        await DeleteAsync(product.Id);
        await AddAsync(product);
    }

    private Task DeleteAsync(string id)
        => Task.FromResult(_products.RemoveAll(p => p.Id.IsEqualTo(id)));
}