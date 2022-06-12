using ORM.EF.DAL.Models;

namespace ORM.EF.DAL.Repositories.Abstraction;

public interface IOrderRepository : IRepository<Order>
{
    public Task<IList<Order>> Fetch(OrderInfo orderInfo);
    public Task BulkDelete(OrderInfo orderInfo);
}