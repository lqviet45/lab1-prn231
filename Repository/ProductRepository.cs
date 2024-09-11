using BusinessObject;
using Repository.abstraction;

namespace Repository;

public class ProductRepository : RepoBase<Product>, IProductRepository
{
    public ProductRepository(MyDbContext context) : base(context)
    {
    }
}