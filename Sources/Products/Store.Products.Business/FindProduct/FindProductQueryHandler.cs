using Store.Products.Contracts;
using Store.Products.Domain;

namespace Store.Products.Business;

internal sealed class FindProductQueryHandler(IProductsRepository products) :
    IRequestHandler<FindProductQuery, ProductModel>
{
    public Task<ProductModel> Handle(FindProductQuery query, CancellationToken _) 
        => products
            .FindAsync(query.Id)
            .EnsureExists(query.Id)
            .MapAsync(ProductModelFactory.Create);
}