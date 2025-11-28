namespace Store.Orders.Business;

public record OrderSummaryModel
{
    public required string Id { get; init; }

    public required DateTimeModel OrderedAt { get; init; }

    public required int TotalProducts { get; init; }
    public required Price TotalPrice { get; init; }
}