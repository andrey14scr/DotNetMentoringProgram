using WebApi.DAL.Repositories.Abstraction;

namespace WebApi.BL.UOW;

public interface IUnitOfWork
{
    public IProductRepository ProductRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public Task<int> SaveChangesAsync();
}