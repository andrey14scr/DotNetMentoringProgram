namespace WebApi.BL.Abstraction;

public interface IService<T>
{
    Task<T> Create(T entity);
    Task<T> GetById(int id);
    Task<IList<T>> GetAll();
    Task<T> Delete(T entity);
    Task<T> Update(T entity);
}