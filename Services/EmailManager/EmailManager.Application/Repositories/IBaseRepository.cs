using EmailManager.Core.Entities;

namespace EmailManager.Application.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> Get(string id);
    Task<List<T>> GetAll();
}