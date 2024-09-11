using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Repository.abstraction;

namespace Repository;

public class RepoBase<T> : IRepoBase<T> where T : class
{
    private readonly MyDbContext _context;

    public RepoBase(MyDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public async Task<T?> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> Create(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await GetById(id);
        if (entity == null)
        {
            throw new Exception("Entity not found");
        }

        _context.Set<T>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}