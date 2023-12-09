using Microsoft.AspNetCore.Identity;
using StudentTransfer.Dal.Entities.Auth;
using StudentTransfer.Utils;

namespace StudentTransfer.Api;

public static class SeedData
{
    public static void SeedRoles(RoleManager<AppRole> roleManager)
    {
        var roles = new List<string>() { RoleConstants.User, RoleConstants.Admin };
        
        roles.ForEach(r => SeedRole(roleManager, r));
    }

    private static void SeedRole(RoleManager<AppRole> roleManager, string roleName)
    {
        if (roleManager.RoleExistsAsync(roleName).Result) return;
        
        var role = new AppRole() { Name = roleName };
        var result = roleManager.CreateAsync(role).Result;

        if (!result.Succeeded)
            throw new Exception("Exception during seeding roles");
    }
}