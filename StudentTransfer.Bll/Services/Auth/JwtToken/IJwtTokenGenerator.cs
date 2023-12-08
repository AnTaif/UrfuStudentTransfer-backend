using StudentTransfer.Dal.Entities.Auth;

namespace StudentTransfer.Bll.Services.Auth.JwtToken;

public interface IJwtTokenGenerator
{
    public Task<string> GenerateTokenAsync(AppUser user);
}