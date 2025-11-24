using Store.Core.Domain;

namespace Store.Core.Business.Products;

internal sealed class FindProductQueryHandler(IProductsRepository products)
    : IRequestHandler<FindProductQuery, GetProductModel>
{
    public async Task<GetProductModel> Handle(FindProductQuery query, CancellationToken _) =>
        await products
            .FindAsync(query.Id)
            .EnsureExists(query.Id)
            .MapAsync(GetProductModel.Create);
}