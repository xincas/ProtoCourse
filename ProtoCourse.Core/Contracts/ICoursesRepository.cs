using ProtoCourse.Core.Models.Course;
using ProtoCourse.Core.Models.Lesson;
using ProtoCourse.Data;

namespace ProtoCourse.Core.Contracts;

public interface ICoursesRepository : IGenericRepository<Course>
{
    Task<CourseDto> GetDetails(Guid id);
    Task<CourseNoSensitiveDto> GetDetailsNoSensitive(Guid id);
    Task AddStudentToCourse(Guid courseId, string userId);
}
