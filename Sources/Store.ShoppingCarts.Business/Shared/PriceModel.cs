namespace Store.ShoppingCarts.Business;

public sealed record PriceModel(decimal Value, string Currency, string Display)
{
    public static PriceModel Create(Price price)
        => new(price.Value, price.Currency, price.ToString());
}