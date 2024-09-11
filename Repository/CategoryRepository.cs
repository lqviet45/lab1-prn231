using BusinessObject;
using Repository.abstraction;

namespace Repository;

public class CategoryRepository : RepoBase<Category>, ICategoryRepository
{
    public CategoryRepository(MyDbContext context) : base(context)
    {
    }
}