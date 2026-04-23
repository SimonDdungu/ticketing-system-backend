using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.Repositories.Interfaces;
namespace Ticketing_backend.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id) =>
        await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _dbSet.ToListAsync();

    public void Add(T entity) =>
        _dbSet.Add(entity);

    public void Update(T entity) =>
        _dbSet.Update(entity);

    public void Delete(T entity) =>
        _dbSet.Remove(entity);

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
}