using LibraryManagementSystem.Model;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IDynatraceLoggerService _logger;

    public AuthController(IUserService userService, IDynatraceLoggerService logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        await _logger.LogAsync("User Log in ", login);

        if (await _userService.IsValidUser(login.Username, login.Password))
        {
            var token = _userService.GenerateJwtToken(login.Username);
            return Ok(new { token });
        }

        return Unauthorized();
    }
}