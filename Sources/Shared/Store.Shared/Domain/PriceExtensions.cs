namespace Store.Shared;

public static class PriceExtensions
{
    public static Price Sum<T>(this IEnumerable<T> source, Func<T, Price> priceSelector)
        => source.Select(priceSelector).Sum();

    public static Price Sum(this IEnumerable<Price> prices)
    {
        return prices.IsNotEmpty()
            ? prices.Aggregate((price1, price2) => price1 + price2)
            : 0;
    }
}