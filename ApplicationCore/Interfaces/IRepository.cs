namespace ApplicationCore.Interfaces;

public interface IRepository<T> where T : class, IAggregateRoot
{
    Task Insert(T entity);
    Task<T> GetAsync(int id);
    Task Update(T entity);
    Task Delete(T entity);
}