namespace Store.Core.Domain;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetAsync(Func<Product, bool> filter);

    Task<Product?> FindAsync(string id);

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);
}