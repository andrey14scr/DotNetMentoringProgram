using AutoMapper;
using WebApi.BL.Abstraction;
using WebApi.BL.UOW;
using WebApi.DAL.Models;
using WebApi.DTO;

namespace WebApi.BL.Implementation;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto> Create(CategoryDto entity)
    {
        var category = _mapper.Map<Category>(entity);
        var newCategory = await _unitOfWork.CategoryRepository.Create(category);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(newCategory);
    }

    public async Task<CategoryDto> GetById(int id)
    {
        var category = await _unitOfWork.CategoryRepository.GetById(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<IList<CategoryDto>> GetAll()
    {
        var categories = await _unitOfWork.CategoryRepository.GetAll();
        return _mapper.Map<IList<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> Delete(CategoryDto entity)
    {
        var category = _mapper.Map<Category>(entity);
        _unitOfWork.CategoryRepository.Delete(category);
        await _unitOfWork.SaveChangesAsync();
        return entity;
    }

    public async Task<CategoryDto> Update(CategoryDto entity)
    {
        var category = _mapper.Map<Category>(entity);
        var updatedCategory = _unitOfWork.CategoryRepository.Update(category);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(updatedCategory);
    }
}