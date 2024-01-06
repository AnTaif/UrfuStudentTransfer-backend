using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Auth;
using StudentTransfer.Dal.Entities.User;
using StudentTransfer.Utils;

namespace StudentTransfer.UnitTests;

public static class MockManager
{
    public static StudentTransferContext GetDbContext()
    {
        return new StudentTransferContext(new DbContextOptionsBuilder<StudentTransferContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);
    }
    
    public static Mock<UserManager<AppUser>> GetMockUserManager()
    {
        var store = new Mock<IUserStore<AppUser>>();

        var userManager = new Mock<UserManager<AppUser>>(store.Object,  null, null, null, null, null, null, null, null);
        userManager.Object.UserValidators.Add(new UserValidator<AppUser>());
        userManager.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());
        
        return userManager;
    }
}