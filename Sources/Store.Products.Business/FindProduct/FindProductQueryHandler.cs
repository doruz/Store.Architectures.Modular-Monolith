using Store.Products.Contracts;
using Store.Products.Domain;

namespace Store.Core.Business.Products;

internal sealed class FindProductQueryHandler(IProductsRepository products) :
    IRequestHandler<FindProductQuery, GetProductModel>,
    IRequestHandler<FindProductRequest, FindProductResponse?>
{
    public async Task<GetProductModel> Handle(FindProductQuery query, CancellationToken _) =>
        await products
            .FindAsync(query.Id)
            .EnsureExists(query.Id)
            .MapAsync(GetProductModel.Create);

    public async Task<FindProductResponse?> Handle(FindProductRequest request, CancellationToken _)
    {
        if (request.Id.IsEmpty())
        {
            return null;
        }

        var product = await products.FindAsync(request.Id);

        return product?.Map(p => new FindProductResponse
        {
            Id = p.Id,
            Name = p.Name,
            Price = PriceModel.Create(p.Price),
            Stock = p.Stock
        });
    }
}