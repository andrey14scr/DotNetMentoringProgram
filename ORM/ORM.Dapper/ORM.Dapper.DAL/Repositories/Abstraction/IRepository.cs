namespace ORM.Dapper.DAL.Repositories.Abstraction;

public interface IRepository<T>
{
    Task Create(T entity);
    Task<T> Select(Guid id);
    Task<IList<T>> Fetch();
    Task Update(T entity);
    Task Delete(T entity);
}