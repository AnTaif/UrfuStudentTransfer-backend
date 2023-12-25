using Microsoft.AspNetCore.Identity;
using StudentTransfer.Bll.Services.Auth.JwtToken;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Auth;
using StudentTransfer.Dal.Entities.User;
using StudentTransfer.Utils;
using StudentTransfer.Utils.Dto.User;

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

    public async Task<GetUserInfoResponse?> GetUserInfoAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return null;

        var fullName = $"{user.LastName} {user.FirstName}";
        if (user.MiddleName != null)
            fullName += $" {user.MiddleName}";

        return new GetUserInfoResponse
        {
            Id = user.Id,
            Email = user.Email!,
            FullName = fullName,
            PhoneNumber = user.PhoneNumber,
            Telegram = user.Telegram
        };
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
            MiddleName = request.MiddleName,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user == null)
            throw new NullReferenceException("already created user is null");

        await _userManager.AddToRoleAsync(user, RoleConstants.User);

        var token = await _jwtTokenGenerator.GenerateTokenAsync(user);

        var fullName = $"{user.LastName} {user.FirstName}";
        if (user.MiddleName != null)
            fullName += $" {user.MiddleName}";
        
        var response = new RegistrationResponse(
            user.Id, 
            fullName,
            user.Email ?? "", 
            token);

        return response;
    }

    public async Task<LoginResponse?> SignInAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return null;

        var loginSuccess = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

        if (!loginSuccess.Succeeded)
            return null;

        var token = await _jwtTokenGenerator.GenerateTokenAsync(user);
        
        var fullName = $"{user.LastName} {user.FirstName}";
        if (user.MiddleName != null)
            fullName += $" {user.MiddleName}";

        return new LoginResponse(user.Id, fullName, user.Email!, token);
    }

    public async Task<IdentityResult> ChangeEmailAndUsernameAsync(ChangeEmailRequest request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
            return IdentityResult.Failed(new IdentityError {Description = "User not found"});

        user.Email = request.NewEmail;
        user.UserName = request.NewEmail;

        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> ChangeUserInfoAsync(ChangeUserInfoRequest request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
            return IdentityResult.Failed(new IdentityError {Description = "User not found"});

        if (request.Email != null)
        {
            user.Email = request.Email;
            user.UserName = request.Email;
        }

        if (request.FullName != null)
        {
            var splitFullName = request.FullName.Split(' ');

            var lastName = splitFullName[0];
            var firstName = splitFullName[1];
            
            user.FirstName = firstName;
            user.LastName = lastName;

            try
            {
                var middleName = splitFullName[2];
                user.MiddleName = middleName;
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        if (request.Telegram != null)
        {
            user.Telegram = request.Telegram;
        }

        if (request.PhoneNumber != null)
        {
            user.PhoneNumber = request.PhoneNumber;
        }

        return await _userManager.UpdateAsync(user);
    }
}