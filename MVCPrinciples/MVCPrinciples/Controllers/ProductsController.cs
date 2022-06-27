using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPrinciples.DAL;
using MVCPrinciples.DAL.Models;

namespace MVCPrinciples.Controllers;

public class ProductsController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly NorthWindContext _context;

    public ProductsController(IConfiguration configuration, NorthWindContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        return View(products);
    }

    public IActionResult Info()
    {
        var maxPerPage = Convert.ToInt32(_configuration["MaxOnPage"]);
        
        var products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier);

        IQueryable<Product> query = products;
        if (maxPerPage != 0)
        {
            query = products.Take(maxPerPage);
        }

        return View(query.ToList());
    }

    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
        ViewData["SupplierId"] = new SelectList(_context.Suppliers.ToList(), "SupplierId", "CompanyName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", product.CategoryId);
        ViewData["SupplierId"] = new SelectList(_context.Suppliers.ToList(), "SupplierId", "CompanyName", product.SupplierId);
        return View(product);
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id == null || _context.Products == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", product.CategoryId);
        ViewData["SupplierId"] = new SelectList(_context.Suppliers.ToList(), "SupplierId", "CompanyName", product.SupplierId);
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault())
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", product.CategoryId);
        ViewData["SupplierId"] = new SelectList(_context.Suppliers.ToList(), "SupplierId", "CompanyName", product.SupplierId);
        return View(product);
    }
}