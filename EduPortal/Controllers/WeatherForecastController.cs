using EduPortal.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class WeatherForecastController : ControllerBase
{
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("Admin")]
    public async Task<IActionResult> GetAdmin()
    {
        return Ok(new { Status = "Success", Message = "This is admin" });
    }

    [Authorize(Roles = UserRoles.User)]
    [HttpGet("User")]
    public async Task<IActionResult> GetUser()
    {
        return Ok(new { Status = "Success", Message = "This is user" });
    }
}
