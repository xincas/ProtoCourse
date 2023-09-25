using ProtoCourse.Core.Models;
using ProtoCourse.Data;

namespace ProtoCourse.Core.Contracts;

public interface ILessonsRepository : IGenericRepository<Lesson>
{
    Task<List<Lesson>> GetAllLessonsAsync(Guid courseId);
    Task<List<TResult>> GetAllLessonsAsync<TResult>(Guid courseId);
    Task<PagedResult<TResult>> GetAllLessonsAsync<TResult>(Guid courseId, QueryParameters queryParameters);
}
