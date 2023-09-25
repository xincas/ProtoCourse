using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoCourse.Core.Contracts;
using ProtoCourse.Core.Models.Course;
using ProtoCourse.Core.Models;
using ProtoCourse.Core.Repository;
using ProtoCourse.Core.Models.Lesson;
using ProtoCourse.Core.Exceptions;

namespace ProtoCourse.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ILogger<LessonsController> _logger;
    private readonly ILessonsRepository _lessonsRepository;
    private readonly IUserRepository _userRepository;

    public LessonsController(ILessonsRepository lessonsRepository, ILogger<LessonsController> logger, IUserRepository userRepository)
        => (_logger, _lessonsRepository, _userRepository) = (logger, lessonsRepository, userRepository);

    #region AuthorAccess

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateLesson(CreateLessonDto createLessonDto)
    {
        var userId = HttpContext.User.FindFirst("uid")!.Value;

        if (!await _userRepository.IsUserAuthorOfCourse(userId, createLessonDto.CourseId)) throw new ForbiddenException();

        var lessonDto = await _lessonsRepository.AddAsync<CreateLessonDto, LessonDto>(createLessonDto);
        return CreatedAtAction(nameof(GetLesson), new { id = lessonDto.Id }, lessonDto);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> UpdateLesson(Guid id, UpdateLessonDto updateLessonDto)
    {
        var userId = HttpContext.User.FindFirst("uid")!.Value;

        if (!await _userRepository.IsUserAuthorOfCourse(userId, id)) throw new ForbiddenException();

        await _lessonsRepository.UpdateAsync(id, updateLessonDto);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> DeleteLesson(Guid id)
    {
        var userId = HttpContext.User.FindFirst("uid")!.Value;
        var courseId = (await _lessonsRepository.GetAsync(id)).CourseId;

        if (!await _userRepository.IsUserAuthorOfCourse(userId, courseId)) throw new ForbiddenException();

        await _lessonsRepository.DeleteAsync(id);
        return NoContent();
    }

    #endregion

    #region StudentAccess

    [HttpGet("course/{id:guid}/get-all")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<LessonDto>>> GetLessons(Guid id)
    {
        var userId = HttpContext.User.FindFirst("uid")!.Value;

        if (!await _userRepository.IsUserStudentOfCourse(userId, id)) throw new ForbiddenException();

        var courses = await _lessonsRepository.GetAllLessonsAsync<LessonDto>(id);
        return Ok(courses);
    }

    [HttpGet("course/{id:guid}")]
    [Authorize]
    public async Task<ActionResult<PagedResult<LessonDto>>> GetPagedLessons(Guid id, [FromQuery] QueryParameters queryParameters)
    {
        var userId = HttpContext.User.FindFirst("uid")!.Value;

        if (!await _userRepository.IsUserStudentOfCourse(userId, id)) throw new ForbiddenException();

        var courses = await _lessonsRepository.GetAllAsync<LessonDto>(queryParameters);
        return Ok(courses);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<LessonDto>> GetLesson(Guid id)
    {
        var lessonDto = await _lessonsRepository.GetAsync<LessonDto>(id);
        var userId = HttpContext.User.FindFirst("uid")!.Value;

        if (!await _userRepository.IsUserStudentOfCourse(userId, lessonDto.CourseId) ||
            !await _userRepository.IsUserAuthorOfCourse(userId, lessonDto.CourseId))
            throw new ForbiddenException();

        return Ok(lessonDto);
    }

    #endregion
}
