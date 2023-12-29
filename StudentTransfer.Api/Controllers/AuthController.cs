using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Bll.Services.Auth.User;
using StudentTransfer.Utils.Dto.User;

namespace StudentTransfer.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        var response = await _userService.RegisterUserAsync(request);

        if (response == null)
            return BadRequest();
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _userService.SignInAsync(request);

        if (response == null)
            return NotFound();
        return Ok(response);
    }
}