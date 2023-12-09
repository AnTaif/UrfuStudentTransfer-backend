using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using StudentTransfer.Dal.Entities.Application;

namespace StudentTransfer.Dal.Entities.Auth;

public class AppUser : IdentityUser<Guid>
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;
    
    public List<ApplicationEntity>? Applications { get; set; } 
}