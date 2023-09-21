using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProtoCourse.Core.Contracts;
using ProtoCourse.Core.Exceptions;
using ProtoCourse.Core.Models.Course;
using ProtoCourse.Core.Models.Lesson;
using ProtoCourse.Data;

namespace ProtoCourse.Core.Repository;

public class CoursesRepository : GenericRepository<Course>, ICoursesRepository
{
    public CoursesRepository(CourseDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task AddStudentToCourse(Guid courseId, string userId)
    {
        var course = await _context.Courses.Include(c => c.Students).FirstOrDefaultAsync(c => c.Id == courseId)
            ?? throw new NotFoundException(nameof(Course), courseId);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new NotFoundException(nameof(User), userId);

        course.Students.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<CourseDto> GetDetails(Guid id)
    {
        var course = await _context.Courses
            .Include(c => c.Author)
            .Include(c => c.Students)
            .Include(c => c.Lessons)
            .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException(nameof(GetDetails), id);

        return course;
    }

    public async Task<CourseNoSensitiveDto> GetDetailsNoSensitive(Guid id)
    {
        var course = await _context.Courses
            .Include(c => c.Lessons)
            .Include(c => c.Students)
            .ProjectTo<CourseNoSensitiveDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException(nameof(Course), id);

        return course;
    }

    public async Task<IEnumerable<LessonDto>> GetLessons(Guid id, string userId)
    {
        var course = await _context.Courses
            .Include(c => c.Lessons)
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException(nameof(Course), id);
        //TODO Create forbiden exception 
        if (course.Students.Any(s => s.Id == userId)) throw new BadRequestException("");

        return course.Lessons.AsQueryable()
            .ProjectTo<LessonDto>(_mapper.ConfigurationProvider);
    }

    public async Task<IEnumerable<LessonNoSensitiveDto>> GetNoSensitiveLessons(Guid courseId)
    {
        var course = await _context.Courses
            .Include(c => c.Lessons)
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.Id == courseId)
            ?? throw new NotFoundException(nameof(Course), courseId);

        return course.Lessons.AsQueryable()
            .ProjectTo<LessonNoSensitiveDto>(_mapper.ConfigurationProvider);
    }
}
