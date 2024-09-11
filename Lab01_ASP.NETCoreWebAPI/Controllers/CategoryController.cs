using Microsoft.AspNetCore.Mvc;
using Services.abstraction;

namespace Lab01_ASP.NETCoreWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService  _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetAll();
        return Ok(categories);
    }
}