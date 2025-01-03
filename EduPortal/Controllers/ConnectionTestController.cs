using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class ConnectionTestController : ControllerBase
{
    private readonly IAuthenticateService _authenticateService;
    public ConnectionTestController(IAuthenticateService authenticateService)
    {
        _authenticateService = authenticateService;
    }
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("AdminList")]
    public async Task<IActionResult> GetAdmin()
    {
        var adminList = _authenticateService.GetListByUserTypeAsync("Admin");
        return  Ok(new { adminList });
    }

    [Authorize(Roles = UserRoles.User)]
    [HttpGet("UserList")]
    public async Task<IActionResult> GetUser()
    {
        return Ok(new { Status = "Success", Message = "This is user" });
    }
}
