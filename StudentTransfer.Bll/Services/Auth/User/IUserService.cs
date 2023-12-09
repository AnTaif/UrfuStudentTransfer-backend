using StudentTransfer.Utils.Dto.Auth;

namespace StudentTransfer.Bll.Services.Auth.User;

public interface IUserService
{
    public Task<RegistrationResponse?> RegisterUserAsync(RegistrationRequest request);

    public Task<LoginResponse?> SignInAsync(LoginRequest request);
}