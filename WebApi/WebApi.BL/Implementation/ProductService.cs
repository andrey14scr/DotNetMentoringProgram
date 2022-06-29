using AutoMapper;
using WebApi.BL.Abstraction;
using WebApi.BL.UOW;
using WebApi.DAL.Models;
using WebApi.DTO;

namespace WebApi.BL.Implementation;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductDto> Create(ProductDto entity)
    {
        var product = _mapper.Map<Product>(entity);
        await _unitOfWork.ProductRepository.Create(product);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<ProductDto> GetById(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<IList<ProductDto>> GetAll()
    {
        var products = await _unitOfWork.ProductRepository.GetAll();
        return _mapper.Map<IList<ProductDto>>(products);
    }

    public async Task<ProductDto> Delete(ProductDto entity)
    {
        var product = _mapper.Map<Product>(entity);
        _unitOfWork.ProductRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<ProductDto> Update(ProductDto entity)
    {
        var product = _mapper.Map<Product>(entity);
        _unitOfWork.ProductRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<IList<ProductDto>> GetAll(int page, int size, int? categoryId)
    {
        var products = await _unitOfWork.ProductRepository.GetPage(page, size, categoryId);
        return _mapper.Map<IList<ProductDto>>(products);
    }
}