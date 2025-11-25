using Store.Shared;
using Store.ShoppingCarts.Business;

[ApiRoute("customers/current/shopping-carts/current")]
public sealed class CustomersShoppingCartsController(IMediator mediator) : BaseApiController(mediator)
{
    /// <summary>
    /// Get cart details of authenticated customer.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<GetCustomerCartQueryResult>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentCart() 
        => await HandleQuery(new GetCustomerCartQuery());

    /// <summary>
    /// Clear cart details of authenticated customer.
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> ClearCurrentCart()
        => await HandleCommand(new ClearCustomerCartCommand());

    /// <summary>
    /// Update cart details of authenticated customer.
    /// </summary>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCurrentCart([FromBody] UpdateCustomerCartLineModel[] lines)
        => await HandleCommand(new UpdateCustomerCartCommand(lines));
}