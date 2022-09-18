using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class EfRepository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
{
    private readonly OVitrinDbContext _db;
    private readonly DbSet<T> _entities;

    public EfRepository(OVitrinDbContext db)
    {
        _db = db;
        _entities = db.Set<T>();
    }

    public virtual IQueryable<T> GetAll()
    {
        return _entities;
    }

    public async Task DeleteAsync(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        
        _entities.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<T?> GetAsync(int id)
    {
        return await _entities.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        
        _db.Add(entity);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        
        _entities.Update(entity);
        await _db.SaveChangesAsync();
    }
}