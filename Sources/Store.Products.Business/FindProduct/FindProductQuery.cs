using Store.Products.Contracts;

namespace Store.Core.Business.Products;

public sealed record FindProductQuery(string Id) : IRequest<ProductModel>;