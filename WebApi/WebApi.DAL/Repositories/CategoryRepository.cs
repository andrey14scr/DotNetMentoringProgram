using Microsoft.EntityFrameworkCore;
using WebApi.DAL.Models;
using WebApi.DAL.Repositories.Abstraction;

namespace WebApi.DAL.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly NorthwindContext _northwindContext;

    public CategoryRepository(NorthwindContext northwindContext)
    {
        _northwindContext = northwindContext;
    }

    public async Task Create(Category entity)
    {
        await _northwindContext.Categories.AddAsync(entity);
    }

    public async Task<Category> GetById(int id)
    {
        return await _northwindContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
    }

    public async Task<IList<Category>> GetAll()
    {
        return await _northwindContext.Categories.AsNoTracking().ToListAsync();
    }

    public void Delete(Category entity)
    {
        _northwindContext.Categories.Remove(entity);
    }

    public void Update(Category entity)
    {
        _northwindContext.Categories.Update(entity);
    }
}