using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProtoCourse.Core.Contracts;
using ProtoCourse.Core.Exceptions;
using ProtoCourse.Core.Models;
using ProtoCourse.Data;

namespace ProtoCourse.Core.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly CourseDbContext _context;
    protected readonly IMapper _mapper;

    public GenericRepository(CourseDbContext context, IMapper mapper) =>
        (_context, _mapper) = (context, mapper);

    public async Task<T> AddAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
    {
        var entity = _mapper.Map<T>(source);

        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<TResult>(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetAsync(id);

        if (entity is null)
        {
            throw new NotFoundException(typeof(T).Name, id);
        }

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(Guid id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters)
    {
        var totalSize = await _context.Set<T>().CountAsync();
        var items = await _context.Set<T>()
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

    public async Task<List<TResult>> GetAllAsync<TResult>()
    {
        return await _context.Set<T>()
            .ProjectTo<TResult>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<T> GetAsync(Guid? id)
    {
        var result = await _context.Set<T>().FindAsync(id);
        if (result is null)
        {
            throw new NotFoundException(typeof(T).Name, id.HasValue ? id : "No Key Provided");
        }

        return result;
    }

    public async Task<TResult> GetAsync<TResult>(Guid? id)
    {
        var result = await _context.Set<T>().FindAsync(id);
        if (result is null)
        {
            throw new NotFoundException(typeof(T).Name, id.HasValue ? id : "No Key Provided");
        }

        return _mapper.Map<TResult>(result);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync<TSource>(Guid id, TSource source) where TSource : IBaseDto
    {
        if (id != source.Id)
        {
            throw new BadRequestException("Invalid Id used in request");
        }

        var entity = await GetAsync(id);

        if (entity == null)
        {
            throw new NotFoundException(typeof(T).Name, id);
        }

        _mapper.Map(source, entity);
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
}
