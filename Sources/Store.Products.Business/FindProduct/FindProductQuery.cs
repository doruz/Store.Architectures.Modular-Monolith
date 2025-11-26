using Store.Products.Contracts;

namespace Store.Products.Business;

public sealed record FindProductQuery(string Id) : IRequest<ProductModel>;