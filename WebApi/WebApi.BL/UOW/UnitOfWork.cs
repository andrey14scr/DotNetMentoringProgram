using WebApi.DAL;
using WebApi.DAL.Repositories.Abstraction;

namespace WebApi.BL.UOW;

public class UnitOfWork : IUnitOfWork
{
    private readonly NorthwindContext _northwindContext;
    public IProductRepository ProductRepository { get; }
    public ICategoryRepository CategoryRepository { get; }

    public UnitOfWork(NorthwindContext northwindContext, IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _northwindContext = northwindContext;
        ProductRepository = productRepository;
        CategoryRepository = categoryRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _northwindContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _northwindContext?.Dispose();
        GC.SuppressFinalize(this);
    }
}