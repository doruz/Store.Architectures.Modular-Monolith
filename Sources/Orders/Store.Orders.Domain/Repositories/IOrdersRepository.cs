namespace Store.Orders.Domain;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetAllAsync(string customerId);

    Task<Order?> FindAsync(string customerId, string id);

    Task SaveAsync(Order order);
}