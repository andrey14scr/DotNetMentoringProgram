using ORM.Dapper.DAL.Models;

namespace ORM.Dapper.DAL.Repositories.Abstraction;

public interface IOrderRepository : IRepository<Order>
{
    public Task<IList<Order>> Fetch(OrderInfo orderInfo);
    public Task BulkDelete(OrderInfo orderInfo);
}