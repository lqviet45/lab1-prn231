using BusinessObject;

namespace Services.abstraction;

public interface IProductService
{
    public Task<List<Product>> GetAll();
    public Task<Product?> GetById(int id);
    public Task<Product> Create(Product entity);
    public Task<Product?> Update(Product entity);
    public Task<bool> Delete(int id);
}