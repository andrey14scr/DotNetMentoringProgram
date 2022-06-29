using WebApi.DAL.Models;

namespace WebApi.DAL.Repositories.Abstraction;

public interface IProductRepository : IRepository<Product>
{
    Task<IList<Product>> GetPage(int page = 0, int size = 10, int? categoryId = null);
}