using Microsoft.AspNetCore.Mvc;
using WebApi.BL.Abstraction;
using WebApi.DTO;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(int page = 0, int size = 10, int? categoryId = null)
    {
        var products = await _productService.GetAll(page, size, categoryId);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _productService.GetById(id);
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> PutProduct(ProductDto productDto)
    {
        var product = await _productService.Update(productDto);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
    {
        var product = await _productService.Create(productDto);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(ProductDto productDto)
    {
        var product = await _productService.Delete(productDto);
        return Ok(product);
    }
}