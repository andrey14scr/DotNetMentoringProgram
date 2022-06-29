using WebApi.DAL.Models;

namespace WebApi.DAL.Repositories.Abstraction;

public interface IRepository<T>
{
    Task Create(T entity);
    Task<T> GetById(int id);
    Task<IList<T>> GetAll();
    void Delete(T entity);
    void Update(T entity);
}