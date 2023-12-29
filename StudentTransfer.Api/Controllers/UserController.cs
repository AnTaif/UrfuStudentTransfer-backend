using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using StudentTransfer.Bll.Services.Auth.User;
using StudentTransfer.Utils.Dto.User;

namespace StudentTransfer.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<GetUserInfoResponse>> GetCurrentUserInfo()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid)!;

        var response = await _userService.GetUserInfoAsync(userId);

        if (response == null)
            return BadRequest();
        return Ok(response);
    }
    
    [HttpGet("id")]
    public async Task<ActionResult<GetUserInfoResponse>> GetUserInfo(Guid id)
    {
        var userId = id.ToString();
        
        var response = await _userService.GetUserInfoAsync(userId);

        if (response == null)
            return BadRequest();
        return Ok(response);
    }

    [HttpPut("info")]
    public async Task<IActionResult> ChangeUserInfo(ChangeUserInfoRequest request)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid)!;
        var result = await _userService.ChangeUserInfoAsync(request, userId);

        if (result.Succeeded)
            return NoContent();

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }
    
    // [HttpPut("change-email")]
    // public async Task<IActionResult> ChangeEmail(ChangeEmailRequest request)
    // {
    //     var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid)!;
    //     var result = await _userService.ChangeEmailAndUsernameAsync(request, userId);
    //
    //     if (result.Succeeded)
    //         return NoContent();
    //
    //     foreach (var error in result.Errors)
    //     {
    //         ModelState.AddModelError(string.Empty, error.Description);
    //     }
    //
    //     return BadRequest();
    // }
}