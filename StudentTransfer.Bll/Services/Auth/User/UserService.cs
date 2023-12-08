using Microsoft.AspNetCore.Identity;
using StudentTransfer.Bll.Services.Auth.JwtToken;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Auth;
using StudentTransfer.Utils;
using StudentTransfer.Utils.Dto.Auth;

namespace StudentTransfer.Bll.Services.Auth.User;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserService(
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, 
        RoleManager<AppRole> roleManager,
        IJwtTokenGenerator jwtTokenGenerator
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<RegistrationResponse?> RegisterUserAsync(RegistrationRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user != null)
            return null;

        user = new AppUser
        {
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user == null)
            throw new NullReferenceException("already created user is null");

        await _userManager.AddToRoleAsync(user, RoleConstants.User);

        var token = await _jwtTokenGenerator.GenerateTokenAsync(user);

        var response = new RegistrationResponse(
            user.Id, 
            user.FirstName, 
            user.LastName, 
            user.Email ?? "", 
            token);

        return response;
    }

    public async Task<LoginResponse?> SignInAsync(LoginRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SignOutAsync()
    {
        throw new NotImplementedException();
    }
}