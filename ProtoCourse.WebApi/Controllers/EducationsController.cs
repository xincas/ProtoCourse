using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProtoCourse.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EducationsController : ControllerBase
{

}
