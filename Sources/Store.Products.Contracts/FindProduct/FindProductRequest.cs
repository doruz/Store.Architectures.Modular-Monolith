using MediatR;

namespace Store.Products.Contracts
{
    public sealed record FindProductRequest(string Id) : IRequest<ProductModel>;
}
