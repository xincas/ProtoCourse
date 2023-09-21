using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoCourse.Core.Contracts;
using ProtoCourse.Core.Models;
using ProtoCourse.Core.Models.Course;

namespace ProtoCourse.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ILogger<CoursesController> _logger;
    private readonly ICoursesRepository _courseRepository;

    public CoursesController(ICoursesRepository courseRepository, ILogger<CoursesController> logger)
        => (_logger, _courseRepository) = (logger, courseRepository);

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
    {
        var courses = await _courseRepository.GetAllAsync();
        return Ok(courses);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<GetCourseDto>>> GetPagedCourses([FromQuery] QueryParameters queryParameters)
    {
        var courses = await _courseRepository.GetAllAsync<GetCourseDto>(queryParameters);
        return Ok(courses);
    }

    [HttpGet("{id:guid}/lessons")]
    [Authorize]
    public async Task<ActionResult<PagedResult<GetCourseDto>>> GetLessonsOfCourse(Guid id)
    {
        var userId = HttpContext.User.FindFirst("Id")?.Value;
        var courses = await _courseRepository.GetLessons(id, userId);
        return Ok(courses);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<CourseDto>> GetCourse(Guid id)
    {
        var course = await _courseRepository.GetAsync(id);
        return Ok(course);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateCourse(CreateCourseDto createCourseDto)
    {
        var course = await _courseRepository.AddAsync<CreateCourseDto, GetCourseDto>(createCourseDto);
        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }

    [HttpPost("{id:guid}/buy")]
    [Authorize]
    public async Task<ActionResult> BuyCourse(Guid id)
    {
        _logger.LogInformation($"User {User.Identity?.Name} tries to buy course {id}");
        _logger.LogInformation($"User claims \n{User.Claims.Select(it => $"\t{it.Type} - {it.Value}\n")}");
        _logger.LogInformation($"User id = {HttpContext.User.FindFirst("Id")?.Value}");
        var userId = HttpContext.User.FindFirst("Id")?.Value;
        await _courseRepository.AddStudentToCourse(id, userId!);
        return Ok();
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> UpdateCourse(Guid id, UpdateCourseDto updateCourseDto)
    {
        await _courseRepository.UpdateAsync(id, updateCourseDto);
        return Ok();
    }

    [HttpDelete]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> DeleteCourse(Guid id)
    {
        await _courseRepository.DeleteAsync(id);
        return NoContent();
    }
}
