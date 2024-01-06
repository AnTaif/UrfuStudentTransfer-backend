using Microsoft.AspNetCore.Identity;
using Moq;
using Moq.Language.Flow;
using StudentTransfer.Dal.Entities.User;
using StudentTransfer.Utils;

namespace StudentTransfer.UnitTests;

public static class MockExtensions
{
    public static List<AppUser> GenerateAndSetupUsers(this Mock<UserManager<AppUser>> mockUserManager, int userCount, List<List<string>?>? roles = null)
    {
        var userRoleDictionary = new Dictionary<Guid, List<string>>();
        var users = new List<AppUser>();
        for (var i = 0; i < userCount; i++)
        {
            var user = new AppUser
                { Id = Guid.NewGuid(), UserName = "UserName", FirstName = "FirstName", LastName = "LastName", Email = "123" };
            users.Add(user);
            userRoleDictionary.Add(user.Id, roles?[i] ?? new List<string> { RoleConstants.User });
        }
        
        mockUserManager
            .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((string id) => users.FirstOrDefault(user => user.Id == Guid.Parse(id)));
        
        mockUserManager
            .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((string email) => users.FirstOrDefault(user => user.Email == email));
        
        mockUserManager
            .Setup(x => x.GetRolesAsync(It.IsAny<AppUser>()))
            .ReturnsAsync((AppUser user) => userRoleDictionary[user.Id]);

        return users;
    }
    
    public static void SetupUsers(this Mock<UserManager<AppUser>> mockUserManager, List<AppUser> users, List<List<string>?>? roles = null)
    {
        var userRoleDictionary = new Dictionary<Guid, List<string>>();
        for (var i = 0; i < users.Count; i++)
        {
            userRoleDictionary.Add(users[i].Id, roles?[i] ?? new List<string> { RoleConstants.User });
        }
        
        mockUserManager
            .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((string id) => users.FirstOrDefault(user => user.Id == Guid.Parse(id)));
        
        mockUserManager
            .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((string email) => users.FirstOrDefault(user => user.Email == email));
        
        mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<AppUser>()))
            .ReturnsAsync((AppUser user) => userRoleDictionary[user.Id]);
    }
}