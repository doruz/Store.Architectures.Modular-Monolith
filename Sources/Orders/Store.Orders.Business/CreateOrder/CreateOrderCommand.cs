using System.ComponentModel.DataAnnotations;

namespace Store.Orders.Business;

public sealed record CreateOrderCommand(CreateOrderLineModel[] Lines) : IRequest<IdModel>
{
    internal IEnumerable<CreateOrderLineModel> ValidLines => Lines.Where(line => line.Quantity > 0);

    internal void EnsureIsNotEmpty()
    {
        if (ValidLines.IsEmpty())
        {
            throw AppError.BadRequest("order_cannot_be_empty");
        }
    }
}

public sealed record CreateOrderLineModel
{
    [Required(ErrorMessage = ValidationMessages.Required)]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxLength)]
    public required string ProductId { get; init; }

    [Range(0, 10, ErrorMessage = ValidationMessages.Range)]
    public required int Quantity { get; init; }
}