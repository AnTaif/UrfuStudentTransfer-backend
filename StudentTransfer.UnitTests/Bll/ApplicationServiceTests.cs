using StudentTransfer.Bll.Services.Application;
using StudentTransfer.Dal.Entities.Vacant;
using StudentTransfer.Dal.Enums;
using StudentTransfer.Utils.Dto.Application;
using static StudentTransfer.UnitTests.MockDataGenerator;

namespace StudentTransfer.UnitTests.Bll;

public class ApplicationServiceTests
{
    [Fact]
    public async Task GetAllAsync_WithSingleUser()
    {
        // Arrange
        var dbContext = GetDbContext();
        
        var userId = Guid.NewGuid();
        var mockUserManager = GetMockUserManager(userId);
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        var user = await mockUserManager.Object.FindByIdAsync(userId.ToString());
        
        var applications = GenerateUserApplications(user!, 2);
        await dbContext.Applications.AddRangeAsync(applications);
        await dbContext.SaveChangesAsync();
        
        // Act
        var result = await applicationService.GetAllAsync();

        // Assert
        Assert.Equal(applications.Count, result.Count);
    }
    
    [Fact]
    public async Task GetAllAsync_WithManyUsers()
    {
        // Arrange
        var dbContext = GetDbContext();
        
        var userId = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var mockUserManager = GetMockUserManager(userId, userId2);
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        var user1 = await mockUserManager.Object.FindByIdAsync(userId.ToString());
        var user2 = await mockUserManager.Object.FindByIdAsync(userId2.ToString());

        var applications = GenerateUserApplications(user1!, 2);
        applications.AddRange(GenerateUserApplications(user2!, 2));
        await dbContext.Applications.AddRangeAsync(applications);
        await dbContext.SaveChangesAsync();
        
        // Act
        var result = await applicationService.GetAllAsync();

        // Assert
        Assert.Equal(applications.Count, result.Count);
    }
    
    [Fact]
    public async Task GetAllAsync_WithoutApplications()
    {
        // Arrange
        var dbContext = GetDbContext();
        var mockUserManager = GetMockUserManager();
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        
        // Act
        var result = await applicationService.GetAllAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllByUserAsync_WithSingleUser()
    {
        // Arrange
        var dbContext = GetDbContext();
        var userId = Guid.NewGuid();
        var mockUserManager = GetMockUserManager(userId);
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        var user = await mockUserManager.Object.FindByIdAsync(userId.ToString());
        
        var applications = GenerateUserApplications(user!, 2);
        await dbContext.Applications.AddRangeAsync(applications);
        await dbContext.SaveChangesAsync();
        
        // Act
        var result = await applicationService.GetAllByUserAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(applications.Count, result.Count);
    }
    
    [Fact]
    public async Task GetAllByUserAsync_WithOtherUsers()
    {
        // Arrange
        var dbContext = GetDbContext();
        var userId = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var mockUserManager = GetMockUserManager(userId, userId2);
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        var user1 = await mockUserManager.Object.FindByIdAsync(userId.ToString());
        var user2 = await mockUserManager.Object.FindByIdAsync(userId2.ToString());
        
        var applications = GenerateUserApplications(user1!, 2);
        var applications2 = GenerateUserApplications(user2!, 2);
        await dbContext.Applications.AddRangeAsync(applications);
        await dbContext.Applications.AddRangeAsync(applications2);
        await dbContext.SaveChangesAsync();
        
        // Act
        var result = await applicationService.GetAllByUserAsync(userId);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(applications.Count, result.Count);
    }
    
    [Fact]
    public async Task GetAllByUserAsync_WhenUserNotFound()
    {
        // Arrange
        var dbContext = GetDbContext();
        var userId = Guid.NewGuid();
        var mockUserManager = GetMockUserManager(userId);
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        var user = await mockUserManager.Object.FindByIdAsync(userId.ToString());
        
        var applications = GenerateUserApplications(user!, 2);
        await dbContext.Applications.AddRangeAsync(applications);
        await dbContext.SaveChangesAsync();
        var testUserId = Guid.NewGuid();
        
        // Act
        var result = await applicationService.GetAllByUserAsync(testUserId);
    
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnsApplicationDtoById()
    {
        // Arrange
        var dbContext = GetDbContext();
        var userId = Guid.NewGuid();
        var mockUserManager = GetMockUserManager(userId);
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        var user = await mockUserManager.Object.FindByIdAsync(userId.ToString());
        
        var applications = GenerateUserApplications(user!, 3);
        await dbContext.Applications.AddRangeAsync(applications);
        await dbContext.SaveChangesAsync();
    
        var testApplication = dbContext.Applications.Last();
        
        // Act
        var result = await applicationService.GetByIdAsync(testApplication.Id);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(testApplication.Id, result.Id);
        Assert.Equal(testApplication.DirectionId, result.Direction.Id);
        Assert.Equal(testApplication.AppUserId, result.UserId);
    }
    
    [Fact]
    public async Task CreateAsync_ReturnsApplicationDtoOrNull()
    {
        // Arrange
        var dbContext = GetDbContext();
        var userId = Guid.NewGuid();
        var mockUserManager = GetMockUserManager(userId);
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);


        var direction = new VacantDirection
        {
            Code = "123",
            Name = "123",
            Level = EducationLevel.Bachelor,
            Course = 0,
            Form = EducationForm.FullTime,
            FederalBudgets = 0,
            SubjectsBudgets = 0,
            LocalBudgets = 0,
            Contracts = 0
        };
        await dbContext.VacantList.AddAsync(direction);
        await dbContext.SaveChangesAsync();
        var createRequest = new CreateApplicationRequest
        {
            Type = "0",
            DetailedType = "0",
            Date = DateTime.UtcNow,
            DirectionId = dbContext.VacantList.Last().Id
        };
        
        // Act
        var result = await applicationService.CreateAsync(createRequest, userId);
    
        // Assert
        Assert.NotNull(result);
        Assert.Single(dbContext.Applications);
        Assert.Equal(userId, result.UserId);
        Assert.Equal(createRequest.Date, result.InitialDate);
    }
    
    [Fact]
    public async Task CreateAsync_WhenVacantDirectionNotFound()
    {
        // Arrange
        var dbContext = GetDbContext();
        var userId = Guid.NewGuid();
        var mockUserManager = GetMockUserManager(userId);
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);

        var user = await mockUserManager.Object.FindByIdAsync(userId.ToString());
        var createRequest = new CreateApplicationRequest
        {
            Type = "0",
            DetailedType = "0",
            Date = DateTime.UtcNow,
            DirectionId = 0
        };
        
        // Act
        var result = await applicationService.CreateAsync(createRequest, userId);
    
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task CreateAsync_WhenUserNotFound()
    {
        // Arrange
        var dbContext = GetDbContext();
        var userId = Guid.NewGuid();
        var mockUserManager = GetMockUserManager(userId);
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);

        var user = await mockUserManager.Object.FindByIdAsync(userId.ToString());
        var createRequest = new CreateApplicationRequest
        {
            Type = "0",
            DetailedType = "0",
            Date = DateTime.UtcNow,
            DirectionId = 0
        };

        var direction = new VacantDirection
        {
            Id = 0,
            Code = "123",
            Name = "123",
            Level = EducationLevel.Bachelor,
            Course = 0,
            Form = EducationForm.FullTime,
            FederalBudgets = 0,
            SubjectsBudgets = 0,
            LocalBudgets = 0,
            Contracts = 0
        };
        await dbContext.VacantList.AddAsync(direction);
        await dbContext.SaveChangesAsync();
        var testUserId = Guid.NewGuid();
        
        // Act
        var result = await applicationService.CreateAsync(createRequest, testUserId);
    
        // Assert
        Assert.Null(result);
    }
}