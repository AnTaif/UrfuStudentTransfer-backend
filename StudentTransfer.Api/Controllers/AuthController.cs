using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Bll.Services.Auth;
using StudentTransfer.Dal.Entities.Auth;

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
    
    
}