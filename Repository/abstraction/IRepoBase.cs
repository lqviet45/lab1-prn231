using System.Linq.Expressions;

namespace Repository.abstraction;

public interface IRepoBase<T> where T : class
{
    public IQueryable<T> GetAll();
    public Task<T?> GetById(int id);
    public Task<T> Create(T entity);
    public Task<T> Update(T entity);
    public Task<bool> Delete(int id);
}