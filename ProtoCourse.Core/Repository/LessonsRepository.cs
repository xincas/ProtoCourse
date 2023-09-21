using AutoMapper;
using ProtoCourse.Core.Contracts;
using ProtoCourse.Data;

namespace ProtoCourse.Core.Repository;

public class LessonsRepository : GenericRepository<Lesson>, ILessonsRepository
{
    public LessonsRepository(CourseDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
