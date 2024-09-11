using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Repository.abstraction;
using Services.abstraction;

namespace Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _categoryRepository.GetAll()
            .OrderBy(c => c.CategoryName)
            .ToListAsync();
    }
}