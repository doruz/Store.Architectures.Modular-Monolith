namespace Store.Shared;

public sealed record PriceModel(decimal Value, string Currency, string Display)
{
    public static PriceModel operator *(PriceModel price, int quantity) => price with
    {
        Value = price.Value * quantity
    };

    public static PriceModel operator +(PriceModel price1, PriceModel price2) => price1 with
    {
        Value = price1.Value + price2.Value
    };

    public static PriceModel Create(Price price) => new(price.Value, price.Currency, price.ToString());
}

public static class PriceModelExtensions
{
    public static PriceModel Sum<T>(this IEnumerable<T> source, Func<T, PriceModel> priceSelector)
        => source.Select(priceSelector).Sum();

    public static PriceModel Sum(this IEnumerable<PriceModel> prices)
    {
        return prices.IsNotEmpty()
            ? prices.Aggregate((price1, price2) => price1 + price2)
            : PriceModel.Create(new Price(0));
    }
}