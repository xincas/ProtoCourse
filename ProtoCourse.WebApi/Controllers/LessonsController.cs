using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoCourse.Core.Contracts;
using ProtoCourse.Core.Models.Course;
using ProtoCourse.Core.Models;
using ProtoCourse.Core.Repository;
using ProtoCourse.Core.Models.Lesson;

namespace ProtoCourse.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ILogger<LessonsController> _logger;
    private readonly ILessonsRepository _lessonsRepository;

    public LessonsController(ILessonsRepository lessonsRepository, ILogger<LessonsController> logger)
        => (_logger, _lessonsRepository) = (logger, lessonsRepository);

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons()
    {
        var courses = await _lessonsRepository.GetAllAsync();
        return Ok(courses);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<LessonDto>>> GetPagedLessons([FromQuery] QueryParameters queryParameters)
    {
        var courses = await _lessonsRepository.GetAllAsync<LessonDto>(queryParameters);
        return Ok(courses);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LessonDto>> GetLesson(Guid id)
    {
        var course = await _lessonsRepository.GetAsync(id);
        return Ok(course);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateLesson(CreateCourseDto createLessonDto)
    {
        var course = await _lessonsRepository.AddAsync<CreateCourseDto, GetCourseDto>(createLessonDto);
        return CreatedAtAction(nameof(GetLesson), new { id = course.Id }, course);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateLesson(Guid id, UpdateLessonDto updateLessonDto)
    {
        await _lessonsRepository.UpdateAsync(id, updateLessonDto);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> DeleteLesson(Guid id)
    {
        await _lessonsRepository.DeleteAsync(id);
        return NoContent();
    }
}
