using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Webion.Extensions.AspNetCore.Authentication.ClickUp;

namespace Webion.Auth.ClickUp.Api.Controllers;

[ApiController]
[Route("clickup/")]
public class LoginController : ControllerBase
{
    [HttpGet]
    [Route("login")]
    public IActionResult Login()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action(controller: "Login", action: "Callback"),
        };
        return Challenge(properties, ClickUpDefaults.AuthenticationScheme);
    }

    [HttpGet]
    [Route("callback")]
    public IActionResult Callback()
    {
        // Gets the username
        var userName = User.FindFirstValue(ClaimTypes.Name);
        return Ok();
    }
}