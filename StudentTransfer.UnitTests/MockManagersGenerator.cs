using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Auth;
using StudentTransfer.Dal.Entities.User;
using StudentTransfer.Utils;

namespace StudentTransfer.UnitTests;

public static class MockManagersGenerator
{
    public static StudentTransferContext GetDbContext()
    {
        return new StudentTransferContext(new DbContextOptionsBuilder<StudentTransferContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);
    }
    
    public static Mock<UserManager<AppUser>> GetMockUserManager(params (Guid userId, List<string>? roles)[] userTuples)
    {
        var users = userTuples
            .Select(t => new AppUser { Id = t.userId, UserName = t.ToString(), FirstName = "user", LastName = "user" })
            .ToList();

        var store = new Mock<IUserStore<AppUser>>();

        var userManager = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Object.UserValidators.Add(new UserValidator<AppUser>());
        userManager.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());

        userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((string userId) => users.FirstOrDefault(user => user.Id == Guid.Parse(userId)));

        userManager.Setup(x => x.GetRolesAsync(It.IsAny<AppUser>()))
            .ReturnsAsync((AppUser user) => userTuples.FirstOrDefault(t => t.userId == user.Id).roles ?? new List<string>());
        
        return userManager;
    }

    public static Mock<UserManager<AppUser>> GetMockUserManager(params (AppUser user, List<string>? roles)[] userTuples)
    {
        var store = new Mock<IUserStore<AppUser>>();

        var userManager = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Object.UserValidators.Add(new UserValidator<AppUser>());
        userManager.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());

        userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((string userId) => userTuples.FirstOrDefault(t => t.user.Id == Guid.Parse(userId)).user);

        userManager.Setup(x => x.GetRolesAsync(It.IsAny<AppUser>()))
            .ReturnsAsync((AppUser user) => userTuples.FirstOrDefault(t => t.user.Id == user.Id).roles ?? new List<string>());

        return userManager;
    }
}