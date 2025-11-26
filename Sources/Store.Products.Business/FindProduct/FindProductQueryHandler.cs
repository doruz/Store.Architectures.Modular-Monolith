using Store.Products.Business;
using Store.Products.Contracts;
using Store.Products.Domain;

namespace Store.Core.Business.Products;

internal sealed class FindProductQueryHandler(IProductsRepository products) :
    IRequestHandler<FindProductQuery, ProductModel>,
    IRequestHandler<FindProductRequest, ProductModel>
{
    public Task<ProductModel> Handle(FindProductQuery query, CancellationToken _)
        => FindProduct(query.Id);

    public Task<ProductModel> Handle(FindProductRequest request, CancellationToken _)
        => FindProduct(request.Id);

    private Task<ProductModel> FindProduct(string id) =>
        products
            .FindAsync(id)
            .EnsureExists(id)
            .MapAsync(ProductModelFactory.Create);
}