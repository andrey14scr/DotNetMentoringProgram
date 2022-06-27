using Microsoft.AspNetCore.Mvc;
using MVCPrinciples.DAL;

namespace MVCPrinciples.Controllers;

public class CategoriesController : Controller
{
    private readonly NorthWindContext _context;

    public CategoriesController(NorthWindContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }
}