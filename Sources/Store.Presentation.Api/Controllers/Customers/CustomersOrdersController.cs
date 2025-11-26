using Store.Orders.Business;
using Store.Shared;

[ApiRoute("customers/current/orders")]
public sealed class CustomersOrdersController(IMediator mediator) : BaseApiController(mediator)
{
    /// <summary>
    /// Get summaries of all orders made by the authenticated customer.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IEnumerable<OrderSummaryModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersSummary()
        => await HandleQuery(new GetCustomerOrdersQuery());

    /// <summary>
    /// Find details of a specific order made by the authenticated customer.
    /// </summary>
    [HttpGet("{orderId}")]
    [ProducesResponseType<FindCustomerOrderQueryResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindOrderDetails([FromRoute] FindCustomerOrderQuery query)
        => await HandleQuery(query);

    // TODO: add integration tests
    /// <summary>
    /// Create new order for the authenticated customer.
    /// </summary>
    [HttpPost]

    [ProducesResponseType<IdModel>(StatusCodes.Status201Created)]
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status409Conflict)]

    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderLineModel[] lines)
    {
        IdModel newOrder = await Handle(new CreateOrderCommand(lines));

        return CreatedAtAction(nameof(FindOrderDetails), new { OrderId = newOrder.Id }, newOrder);
    }
}