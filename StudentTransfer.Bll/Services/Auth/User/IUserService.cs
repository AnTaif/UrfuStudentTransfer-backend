using Microsoft.AspNetCore.Identity;
using StudentTransfer.Utils.Dto.User;

namespace StudentTransfer.Bll.Services.Auth.User;

public interface IUserService
{
    public Task<GetUserInfoResponse?> GetUserInfoAsync(string userId);
    
    public Task<RegistrationResponse?> RegisterUserAsync(RegistrationRequest request);

    public Task<LoginResponse?> SignInAsync(LoginRequest request);

    public Task<IdentityResult> ChangeEmailAndUsernameAsync(ChangeEmailRequest request, string userId);

    public Task<IdentityResult> ChangeUserInfoAsync(ChangeUserInfoRequest request, string userId);
}