using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Webion.Auth.ClickUp.Api.Controllers;

[ApiController]
[Authorize]
[Route("user_info")]
public sealed class UserInfoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}