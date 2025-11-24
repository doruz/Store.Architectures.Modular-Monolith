using Store.Core.Domain;

namespace Store.Core.Business.Products;

internal sealed class AddProductCommandHandler(IProductsRepository products)
    : IRequestHandler<AddProductCommand, IdModel>
{
    public async Task<IdModel> Handle(AddProductCommand command, CancellationToken _)
    {
        var newProduct = CreateProduct(command);

        await products.AddAsync(newProduct);

        return new IdModel(newProduct.Id);
    }

    private static Product CreateProduct(AddProductCommand command) => new
    (
        command.Name,
        command.Price,
        command.Stock
    );
}