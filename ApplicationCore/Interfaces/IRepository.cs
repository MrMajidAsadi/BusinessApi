namespace ApplicationCore.Interfaces;

public interface IRepository<T> where T : class, IAggregateRoot
{
    Task InsertAsync(T entity);
    Task<T?> GetAsync(int id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}