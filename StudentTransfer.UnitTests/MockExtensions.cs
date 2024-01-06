using Microsoft.AspNetCore.Identity;
using Moq;
using Moq.Language.Flow;
using StudentTransfer.Dal.Entities.User;

namespace StudentTransfer.UnitTests;

public static class MockExtensions
{
    public static List<AppUser> SetupUsers(this Mock<UserManager<AppUser>> mockUserManager, int count)
    {
        var users = new List<AppUser>();
        for (var i = 0; i < count; i++)
        {
            users.Add(new AppUser { Id = Guid.NewGuid(), UserName = "UserName", FirstName = "FirstName", LastName = "LastName" });
        }
        
        mockUserManager
            .Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((string id) => users.FirstOrDefault(user => user.Id == Guid.Parse(id)));

        return users;
    }
}