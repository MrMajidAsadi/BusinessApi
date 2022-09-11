using ApplicationCore.Interfaces;

namespace Infrastructure.Data;

public class EfRepository<T> : IRepository<T> where T : class, IAggregateRoot
{
    public Task Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(T entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(T entity)
    {
        throw new NotImplementedException();
    }
}