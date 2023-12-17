using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using StudentTransfer.Dal.Entities.Application;

namespace StudentTransfer.Dal.Entities.User;

public class AppUser : IdentityUser<Guid>
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;
    
    public string? MiddleName { get; set; }
    
    public string? Telegram { get; set; }
    
    public virtual List<ApplicationEntity>? Applications { get; set; } 
}