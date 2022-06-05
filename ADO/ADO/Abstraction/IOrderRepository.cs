using ADO.Models;

namespace ADO.Abstraction;

public interface IOrderRepository : IRepository<Order>
{
    public Task<IList<Order>> Fetch(OrderInfo orderInfo);
    public Task BulkDelete(OrderInfo orderInfo);
}