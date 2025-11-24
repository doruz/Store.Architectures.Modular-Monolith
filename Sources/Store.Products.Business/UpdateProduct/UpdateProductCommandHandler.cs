using Store.Products.Domain;

namespace Store.Core.Business.Products;

internal sealed class UpdateProductCommandHandler(IProductsRepository products)
    : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand command, CancellationToken _)
    {
        var existingProduct = await products
            .FindAsync(command.Id)
            .EnsureExists(command.Id);

        existingProduct.Update(command.Name, command.Price, command.Stock);

        await products.UpdateAsync(existingProduct);
    }
}