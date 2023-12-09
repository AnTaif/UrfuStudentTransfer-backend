using System.ComponentModel.DataAnnotations;

namespace StudentTransfer.Utils.Dto.Auth;

public class UserDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
}

public class RegistrationRequest
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public record RegistrationResponse(Guid Id, string FirstName, string LastName, string Email, string Token);

public class LoginRequest
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public record LoginResponse(Guid Id, string FirstName, string LastName, string Email, string Token);

