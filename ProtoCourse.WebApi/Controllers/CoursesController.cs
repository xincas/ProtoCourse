using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoCourse.Core.Contracts;
using ProtoCourse.Core.Exceptions;
using ProtoCourse.Core.Models;
using ProtoCourse.Core.Models.Course;
using ProtoCourse.Core.Models.Lesson;

namespace ProtoCourse.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ILogger<CoursesController> _logger;
    private readonly ICoursesRepository _courseRepository;
    private readonly IUserRepository _userRepository;

    public CoursesController(ICoursesRepository courseRepository, ILogger<CoursesController> logger, IUserRepository userRepository)
        => (_logger, _courseRepository, _userRepository) = (logger, courseRepository, userRepository);


    #region PublicAcess

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<CourseNoSensitiveDto>>> GetCourses()
    {
        var courses = await _courseRepository.GetAllAsync<CourseNoSensitiveDto>();
        return Ok(courses);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<CourseNoSensitiveDto>>> GetPagedCourses([FromQuery] QueryParameters queryParameters)
    {
        var courses = await _courseRepository.GetAllAsync<CourseNoSensitiveDto>(queryParameters);
        return Ok(courses);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CourseNoSensitiveDto>> GetCourse(Guid id)
    {
        var course = await _courseRepository.GetAsync<CourseNoSensitiveDto>(id);
        return Ok(course);
    }

    #endregion

    #region AuthorAcess

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateCourse(CreateCourseDto createCourseDto)
    {
        var course = await _courseRepository.AddAsync<CreateCourseDto, GetCourseDto>(createCourseDto);
        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> UpdateCourse(Guid id, UpdateCourseDto updateCourseDto)
    {
        var userId = HttpContext.User.FindFirst("uid")!.Value;

        if (!await _userRepository.IsUserAuthorOfCourse(userId, id)) throw new ForbiddenException();

        await _courseRepository.UpdateAsync(id, updateCourseDto);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> DeleteCourse(Guid id)
    {
        var userId = HttpContext.User.FindFirst("uid")!.Value;

        if (!await _userRepository.IsUserAuthorOfCourse(userId, id)) throw new ForbiddenException();

        await _courseRepository.DeleteAsync(id);
        return NoContent();
    }

    #endregion

    #region StudentAccess

    [HttpPost("{id:guid}/buy")]
    [Authorize]
    public async Task<ActionResult> BuyCourse(Guid id)
    {
        var userId = HttpContext.User.FindFirst("uid")?.Value;
        await _courseRepository.AddStudentToCourse(id, userId!);
        return Ok();
    }

    #endregion
}
