namespace ProtoCourse.Core.Contracts;

public interface IUserRepository
{
    Task<bool> IsUserAuthorOfCourse(string userId, Guid courseId);
    Task<bool> IsUserStudentOfCourse(string userId, Guid courseId);
}
