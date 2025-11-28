namespace Store.Shared;

public record DateTimeModel(DateTime Value, string Label)
{
    public static DateTimeModel Create(DateTime dateTime)
        => new(dateTime, dateTime.ToString("dd MMM yyyy, HH:mm"));
}