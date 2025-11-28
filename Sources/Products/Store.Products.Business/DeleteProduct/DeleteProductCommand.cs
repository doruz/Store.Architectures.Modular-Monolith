namespace Store.Products.Business;

public sealed record DeleteProductCommand(string Id) : IRequest;