namespace ADO.Abstraction;

public interface IRepository<T>
{
    Task<T> Create(T entity);
    Task<T> Select(Guid id);
    Task<IList<T>> Fetch();
    Task<T> Update(T entity);
    Task<T> Delete(T entity);
}