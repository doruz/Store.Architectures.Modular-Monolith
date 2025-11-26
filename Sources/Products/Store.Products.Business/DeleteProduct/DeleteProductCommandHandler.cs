using Store.Products.Domain;

namespace Store.Products.Business;

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