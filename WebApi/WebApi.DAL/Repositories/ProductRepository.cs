using Microsoft.EntityFrameworkCore;
using WebApi.DAL.Models;
using WebApi.DAL.Repositories.Abstraction;

namespace WebApi.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly NorthwindContext _northwindContext;

    public ProductRepository(NorthwindContext northwindContext)
    {
        _northwindContext = northwindContext;
    }

    public async Task Create(Product entity)
    {
        await _northwindContext.Products.AddAsync(entity);
    }

    public async Task<Product> GetById(int id)
    {
        return await _northwindContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
    }

    public async Task<IList<Product>> GetAll()
    {
        return await _northwindContext.Products.AsNoTracking().ToListAsync();
    }

    public void Delete(Product entity)
    {
        _northwindContext.Products.Remove(entity);
    }

    public void Update(Product entity)
    {
        _northwindContext.Products.Update(entity);
    }

    public async Task<IList<Product>> GetPage(int page, int size, int? categoryId)
    {
        var query = _northwindContext.Products.AsQueryable();

        if (categoryId is not null)
        {
            query = query.Where(p => p.CategoryId == categoryId);
        }

        return await query.Skip(page * size)
            .Take(size)
            .AsNoTracking()
            .ToListAsync();
    }
}