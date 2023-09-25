using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProtoCourse.Core.Contracts;
using ProtoCourse.Core.Models;
using ProtoCourse.Data;

namespace ProtoCourse.Core.Repository;

public class LessonsRepository : GenericRepository<Lesson>, ILessonsRepository
{
    public LessonsRepository(CourseDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<Lesson>> GetAllLessonsAsync(Guid courseId)
    {
        return await _context.Lessons.AsQueryable()
            .Where(l => l.CourseId == courseId)
            .OrderBy(l => l.Number)
            .ToListAsync();
    }

    public async Task<List<TResult>> GetAllLessonsAsync<TResult>(Guid courseId)
    {
        return await _context.Lessons.AsQueryable()
            .Where(l => l.CourseId == courseId)
            .OrderBy(l => l.Number)
            .ProjectTo<TResult>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<PagedResult<TResult>> GetAllLessonsAsync<TResult>(Guid courseId, QueryParameters queryParameters)
    {
        var totalSize = await _context.Lessons.CountAsync();
        var items = await _context.Lessons.AsQueryable()
            .Where(l => l.CourseId == courseId)
            .OrderBy(l => l.Number)
            .Skip(queryParameters.StartIndex)
            .Take(queryParameters.PageSize)
            .ProjectTo<TResult>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PagedResult<TResult>
        {
            Items = items,
            PageNumber = queryParameters.PageNumber,
            RecordNumber = queryParameters.PageSize,
            TotalCount = totalSize
        };
    }
}
