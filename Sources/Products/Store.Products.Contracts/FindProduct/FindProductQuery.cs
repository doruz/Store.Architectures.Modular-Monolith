using MediatR;

namespace Store.Products.Contracts
{
    public sealed record FindProductQuery(string Id) : IRequest<ProductModel>;
}
