using AutoMapper;
using ProtoCourse.Core.Models.Course;
using ProtoCourse.Core.Models.Lesson;
using ProtoCourse.Core.Models.User;
using ProtoCourse.Data;

namespace ProtoCourse.Core.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Course, CreateCourseDto>().ReverseMap();
        CreateMap<Course, GetCourseDto>().ReverseMap();
        CreateMap<Course, UpdateCourseDto>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CourseNoSensitiveDto>().ReverseMap();

        CreateMap<Lesson, CreateLessonDto>().ReverseMap();
        CreateMap<Lesson, UpdateLessonDto>().ReverseMap();
        CreateMap<Lesson, LessonDto>().ReverseMap();
        CreateMap<Lesson, LessonNoSensitiveDto>().ReverseMap();

        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserNoSensitiveDto>().ReverseMap();
    }
}
