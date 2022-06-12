using Microsoft.EntityFrameworkCore;
using ORM.EF.DAL.Models;
using ORM.EF.DAL.Repositories.Abstraction;

namespace ORM.EF.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ShopContext _shopContext;

    public OrderRepository(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task Create(Order entity)
    {
        await _shopContext.Orders.AddAsync(entity);
    }

    public async Task<Order> Select(Guid id)
    {
        return await _shopContext.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IList<Order>> Fetch()
    {
        return await _shopContext.Orders.AsNoTracking().ToListAsync();
    }

    public void Update(Order entity)
    {
        _shopContext.Orders.Update(entity);
    }

    public void Delete(Order entity)
    {
        _shopContext.Orders.Remove(entity);
    }

    public async Task<IList<Order>> Fetch(OrderInfo orderInfo)
    {
        return await _shopContext.Orders
            .Where(o => (orderInfo.Status == null || o.Status == orderInfo.Status) && 
                        (orderInfo.ProductId == null || o.ProductId == orderInfo.ProductId) &&
                        (orderInfo.Year == null || o.UpdatedDate.Year == orderInfo.Year) &&
                        (orderInfo.Month == null || o.UpdatedDate.Month == orderInfo.Month))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task BulkDelete(OrderInfo orderInfo)
    {
        var ordersToDelete = await _shopContext.Orders
            .Where(o => (orderInfo.Status == null || o.Status == orderInfo.Status) &&
                        (orderInfo.ProductId == null || o.ProductId == orderInfo.ProductId) &&
                        (orderInfo.Year == null || o.UpdatedDate.Year == orderInfo.Year) &&
                        (orderInfo.Month == null || o.UpdatedDate.Month == orderInfo.Month))
            .ToListAsync();

        _shopContext.Orders.RemoveRange(ordersToDelete);
    }

    public async Task SaveChanges()
    {
        await _shopContext.SaveChangesAsync();
    }
}