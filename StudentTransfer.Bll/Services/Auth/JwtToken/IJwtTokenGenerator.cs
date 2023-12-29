using StudentTransfer.Dal.Entities.User;

namespace StudentTransfer.Bll.Services.Auth.JwtToken;

public interface IJwtTokenGenerator
{
    public Task<string> GenerateTokenAsync(AppUser user);
}