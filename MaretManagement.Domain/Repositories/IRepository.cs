using MaretManagement.Domain.Abstraction;

namespace MaretManagement.Domain.Repositories;

public interface IRepository<T> where T : IAggregate
{
    T Get(int id);
    T Update(T entity);
    T[] GetAll();
    void DeleteAll();
}
