using Microsoft.AspNetCore.Mvc;
using WebApi.BL.Abstraction;
using WebApi.DTO;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var categories = await _categoryService.GetAll();
        return Ok(categories);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category = await _categoryService.GetById(id);
        return Ok(category);
    }
    
    [HttpPut]
    public async Task<ActionResult<CategoryDto>> PutCategory(CategoryDto categoryDto)
    {
        var category = await _categoryService.Update(categoryDto);
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto categoryDto)
    {
        var category = await _categoryService.Create(categoryDto);
        return Ok(category);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(CategoryDto categoryDto)
    {
        var category = await _categoryService.Delete(categoryDto);
        return Ok(category);
    }
}