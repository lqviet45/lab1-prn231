using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Services.abstraction;

namespace Lab01_ASP.NETCoreWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetAll();
        
        return Ok(products);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _productService.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        await _productService.Create(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        var result = await _productService.Update(product);
        
        if (result == null)
        {
            return NotFound();
        }
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productService.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        await _productService.Delete(product.ProductId);
        return NoContent();
    }
}