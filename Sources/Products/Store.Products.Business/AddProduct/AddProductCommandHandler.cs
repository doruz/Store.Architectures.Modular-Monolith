using Store.Products.Domain;

namespace Store.Products.Business;

internal sealed class AddProductCommandHandler(IProductsRepository products)
    : IRequestHandler<AddProductCommand, EntityId>
{
    public async Task<EntityId> Handle(AddProductCommand command, CancellationToken _)
    {
        var newProduct = CreateProduct(command);

        await products.AddAsync(newProduct);

        return newProduct.Id;
    }

    private static Product CreateProduct(AddProductCommand command) => new
    (
        command.Name,
        command.Price,
        command.Stock
    );
}