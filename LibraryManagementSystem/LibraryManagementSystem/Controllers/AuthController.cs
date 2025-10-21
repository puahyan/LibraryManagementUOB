using LibraryManagementSystem.Model;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        if (await _userService.IsValidUser(login.Username, login.Password))
        {
            var token = _userService.GenerateJwtToken(login.Username);
            return Ok(new { token });
        }
        return Unauthorized();
    }
}