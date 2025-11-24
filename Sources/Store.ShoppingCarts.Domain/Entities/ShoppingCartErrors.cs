using Store.Shared;

namespace Store.ShoppingCarts.Domain;

public static class ShoppingCartErrors
{
    public static ShoppingCart EnsureIsNotEmpty(this ShoppingCart? shoppingCart)
    {
        if (shoppingCart is null || shoppingCart.IsEmpty())
        {
            throw AppError.NotFound("shopping_cart_is_empty");
        }

        return shoppingCart;
    }
}