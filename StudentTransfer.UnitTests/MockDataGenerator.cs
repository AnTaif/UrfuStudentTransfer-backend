using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Entities.User;
using StudentTransfer.Dal.Enums;

namespace StudentTransfer.UnitTests;

public static class MockDataGenerator
{
    public static List<ApplicationEntity> GenerateUserApplications(Guid userId, int count = 1)
    {
        var user = new AppUser { Id = userId, UserName = "user", FirstName = "user", LastName = "user" };

        var applications = new List<ApplicationEntity>();
        var direction = GenerateDirection();
        
        for (var i = 0; i < count; i++)
        {
            var application = new ApplicationEntity
            {
                Type = ApplicationType.Recovery,
                DetailedType = ApplicationDetailedType.RecoveryToBudget,
                AppUserId = userId,
                User = user,
                CurrentStatus = Status.Sent,
                InitialDate = DateTime.UtcNow,
                Direction = direction,
                IsActive = true
            };
            
            applications.Add(application);
        }

        return applications;
    }

    public static Direction GenerateDirection()
    {
        var guid = Guid.NewGuid().ToString();
        
        return new Direction
        {
            Code = guid,
            Name = guid,
            Level = EducationLevel.Bachelor,
            Course = 1,
            Form = EducationForm.FullTime
        };
    }
    
    public static StudentTransferContext GetDbContext()
    {
        return new StudentTransferContext(new DbContextOptionsBuilder<StudentTransferContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);
    }
    
    public static Mock<UserManager<AppUser>> GetMockUserManager(params Guid[] userIds)
    {
        var users = userIds
            .Select(guid => new AppUser { Id = guid, UserName = guid.ToString(), FirstName = "user", LastName = "user" })
            .ToList();

        var store = new Mock<IUserStore<AppUser>>();
        store.Setup(x => x.FindByIdAsync(It.IsAny<string>(), It.IsAny<System.Threading.CancellationToken>()))
            .ReturnsAsync((string userId, CancellationToken cancellationToken) => users.FirstOrDefault(u => u.Id.ToString() == userId));

        var userManager = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
        userManager.Object.UserValidators.Add(new UserValidator<AppUser>());
        userManager.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());

        return userManager;
    }
}