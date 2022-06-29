using WebApi.DTO;

namespace WebApi.BL.Abstraction;

public interface IProductService : IService<ProductDto>
{
    public Task<IList<ProductDto>> GetAll(int page, int size, int? categoryId);
}