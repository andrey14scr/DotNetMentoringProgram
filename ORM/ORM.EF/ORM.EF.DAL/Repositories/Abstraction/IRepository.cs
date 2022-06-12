namespace ORM.EF.DAL.Repositories.Abstraction;

public interface IRepository<T>
{
    Task Create(T entity);
    Task<T> Select(Guid id);
    Task<IList<T>> Fetch();
    void Update(T entity);
    void Delete(T entity);

    public Task SaveChanges();
}