using Microsoft.EntityFrameworkCore;
using ProtoCourse.Core.Contracts;
using ProtoCourse.Data;

namespace ProtoCourse.Core.Repository;

public class UserRepository : IUserRepository
{
    private readonly CourseDbContext _context;

    public UserRepository(CourseDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsUserAuthorOfCourse(string userId, Guid courseId)
    {
        User? user = (await _context.Users.Include(u => u.TeachingCourses).FirstOrDefaultAsync(u => u.Id == userId));
        return user?.TeachingCourses.Any(c => c.Id == courseId) ?? false;
    }

    public async Task<bool> IsUserStudentOfCourse(string userId, Guid courseId)
    {
        User? user = (await _context.Users.Include(u => u.StudingCourses).FirstOrDefaultAsync(u => u.Id == userId));
        return user?.TeachingCourses.Any(c => c.Id == courseId) ?? false;
    }
}
