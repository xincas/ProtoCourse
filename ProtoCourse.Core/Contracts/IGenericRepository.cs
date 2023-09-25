using ProtoCourse.Core.Models;
using ProtoCourse.Data;

namespace ProtoCourse.Core.Contracts;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetAsync(Guid? id, IEnumerable<string>? includeFields = null);
    Task<TResult> GetAsync<TResult>(Guid? id, IEnumerable<string>? includeFields = null);
    Task<List<T>> GetAllAsync();
    Task<List<TResult>> GetAllAsync<TResult>();
    Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
    Task<T> AddAsync(T entity);
    Task<TResult> AddAsync<TSource, TResult>(TSource source);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(T entity);
    Task UpdateAsync<TSource>(Guid id, TSource source) where TSource : IBaseDto;
    Task<bool> Exists(Guid id);
}
