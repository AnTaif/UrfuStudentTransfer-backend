using System.ComponentModel.DataAnnotations;

namespace StudentTransfer.Utils.Dto.User;

public class ChangeEmailRequest
{
    [Required] public string NewEmail { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}