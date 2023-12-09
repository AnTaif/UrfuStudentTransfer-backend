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
    private readonly VacantService _vacantService;

    public SeedController(RoleManager<AppRole> roleManager, VacantService vacantService)
    {
        _roleManager = roleManager;
        _vacantService = vacantService;
    }

    [HttpGet] //TODO: Change to HttpPost
    public async Task<IActionResult> Seed()
    {
        await SeedRolesAsync();
        await SeedVacantListAsync();

        return NoContent();
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
            if (await _roleManager.RoleExistsAsync(role)) return;
        
            var appRole = new AppRole() { Name = role };
            var result = await _roleManager.CreateAsync(appRole);

            if (!result.Succeeded)
                throw new Exception("Exception during seeding roles");
        }
    }
}