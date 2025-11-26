using Store.Products.Business;
using Store.Products.Contracts;
using Store.Products.Domain;

namespace Store.Core.Business.Products;

internal sealed class FindProductQueryHandler(IProductsRepository products) :
    IRequestHandler<FindProductQuery, ProductModel>,
    IRequestHandler<FindProductRequest, ProductModel?>
{
    public async Task<ProductModel> Handle(FindProductQuery query, CancellationToken _) =>
        await products
            .FindAsync(query.Id)
            .EnsureExists(query.Id)
            .MapAsync(ProductModelFactory.Create);

    public async Task<ProductModel?> Handle(FindProductRequest request, CancellationToken _)
    {
        var product = await products.FindAsync(request.Id);

        return product.DoesNotExists() 
            ? null
            : product?.Map(ProductModelFactory.Create);
    }
}