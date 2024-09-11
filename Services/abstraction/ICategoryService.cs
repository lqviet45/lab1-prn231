using BusinessObject;

namespace Services.abstraction;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAll();
}