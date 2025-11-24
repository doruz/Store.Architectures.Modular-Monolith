namespace Store.Orders.Business;

public record ValueLabelModel<T>(T Value, string Label)
{
    public override string ToString() => Label;
}

internal static class ValueLabelModelExtensions
{
    internal static ValueLabelModel<DateTime> ToDateTimeModel(this DateTime orderedAt)
        => new(orderedAt, orderedAt.ToString("dd MMM yyyy, HH:mm"));
}