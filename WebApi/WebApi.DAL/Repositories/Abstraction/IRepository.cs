using WebApi.DAL.Models;

namespace WebApi.DAL.Repositories.Abstraction;

public interface IRepository<T>
{
    Task<T> Create(T entity);
    Task<T> GetById(int id);
    Task<IList<T>> GetAll();
    void Delete(T entity);
    T Update(T entity);
}