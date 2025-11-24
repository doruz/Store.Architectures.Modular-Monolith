using Store.Core.Domain;

namespace Store.Core.Business.Products;

internal sealed class DeleteProductCommandHandler(IProductsRepository products)
    : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand command, CancellationToken _)
    {
        var product = await products
            .FindAsync(command.Id)
            .EnsureExists(command.Id);

        product.MarkAsDeleted();

        await products.UpdateAsync(product);
    }
}