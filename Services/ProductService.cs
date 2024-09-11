using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Repository.abstraction;
using Services.abstraction;

namespace Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAll()
    {
        var products = await _productRepository.GetAll().ToListAsync();
        
        return products;
    }

    public async Task<Product?> GetById(int id)
    {
        var product = await _productRepository.GetAll()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);
        
        return product;
    }

    public async Task<Product> Create(Product entity)
    {
        var product = await _productRepository.Create(entity);
        
        return product;
    }

    public async Task<Product?> Update(Product entity)
    {
        var existingProduct = await _productRepository.GetById(entity.ProductId);
        if (existingProduct == null)
        {
            return null;
        }
        existingProduct.ProductName = entity.ProductName;
        existingProduct.CategoryId = entity.CategoryId;
        existingProduct.UnitPrice = entity.UnitPrice;
        var product = await _productRepository.Update(existingProduct);
        
        return product;
    }

    public async Task<bool> Delete(int id)
    {
        var result = await _productRepository.Delete(id);
        
        return result;
    }
}