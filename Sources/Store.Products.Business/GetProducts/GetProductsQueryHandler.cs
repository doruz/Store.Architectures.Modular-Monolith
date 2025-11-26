using Store.Products.Business;
using Store.Products.Contracts;
using Store.Products.Domain;

namespace Store.Core.Business.Products;

internal sealed class GetProductsQueryHandler(IProductsRepository products)
    : IRequestHandler<GetProductsQuery, IEnumerable<ProductModel>>
{
    public async Task<IEnumerable<ProductModel>> Handle(GetProductsQuery query, CancellationToken _)
    {
        var filteredProducts = await products.GetAsync(query.Filter);

        return filteredProducts
            .OrderBy(p => p.Name, StringComparer.InvariantCultureIgnoreCase)
            .Select(ProductModelFactory.Create);
    }
}