using Store.Products.Domain;

namespace Store.Core.Business.Products;

internal sealed class GetProductsQueryHandler(IProductsRepository products)
    : IRequestHandler<GetProductsQuery, IEnumerable<GetProductModel>>
{
    public async Task<IEnumerable<GetProductModel>> Handle(GetProductsQuery query, CancellationToken _)
    {
        var filteredProducts = await products.GetAsync(query.Filter);

        return filteredProducts
            .OrderBy(p => p.Name, StringComparer.InvariantCultureIgnoreCase)
            .Select(GetProductModel.Create);
    }
}