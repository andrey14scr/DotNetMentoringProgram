using Microsoft.EntityFrameworkCore;
using ORM.EF.DAL.Models;
using ORM.EF.DAL.Repositories.Abstraction;

namespace ORM.EF.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShopContext _shopContext;

    public ProductRepository(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task Create(Product entity)
    {
        await _shopContext.Products.AddAsync(entity);
    }

    public async Task<Product> Select(Guid id)
    {
        return await _shopContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IList<Product>> Fetch()
    {
        return await _shopContext.Products.AsNoTracking().ToListAsync();
    }

    public void Update(Product entity)
    {
        _shopContext.Products.Update(entity);
    }

    public void Delete(Product entity)
    {
        _shopContext.Products.Remove(entity);
    }

    public async Task SaveChanges()
    {
        await _shopContext.SaveChangesAsync();
    }
}