using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Bll.Services.Vacant;
using StudentTransfer.Dal.Entities.Auth;
using StudentTransfer.Utils;

namespace StudentTransfer.Api.Controllers;

[ApiController]
[Route("api/seed")]
public class SeedController : ControllerBase
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IVacantService _vacantService;
    private readonly UserManager<AppUser> _userManager;

    public SeedController(RoleManager<AppRole> roleManager, IVacantService vacantService, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _vacantService = vacantService;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Seed()
    {
        await SeedRolesAsync();
        await SeedVacantListAsync();
        await SeedAdminUsersAsync();

        return NoContent();
    }

    private async Task SeedAdminUsersAsync()
    {
        var adminUsers = new List<AppUser>()
        {
            new()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UserName = "adminuser1@mail.ru",
                Email = "adminuser1@mail.ru",
                FirstName = "Admin",
                LastName = "User"
            }
        };

        foreach (var adminUser in adminUsers)
        {
            if (await _userManager.FindByIdAsync(adminUser.Id.ToString()) != null)
                continue;
            
            const string password = "Adminpassword123";
            
            await _userManager.CreateAsync(adminUser, password);
            await _userManager.AddToRoleAsync(adminUser, RoleConstants.Admin);
        }
    }

    private async Task SeedVacantListAsync()
    {
        await _vacantService.UpdateParseAsync();
    }
    
    private async Task SeedRolesAsync()
    {
        var roles = new List<string>() { RoleConstants.User, RoleConstants.Admin };

        foreach (var role in roles)
        {
            if (await _roleManager.RoleExistsAsync(role)) 
                continue;
        
            var appRole = new AppRole() { Name = role };
            var result = await _roleManager.CreateAsync(appRole);

            if (!result.Succeeded)
                throw new Exception("Exception during seeding roles");
        }
    }
}