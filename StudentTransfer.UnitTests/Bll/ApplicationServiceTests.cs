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
        var dbContext = MockManager.GetDbContext();
        
        var mockUserManager = MockManager.GetMockUserManager();
        var user = mockUserManager.SetupUsers(1).Single();
        
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        
        var applications = GenerateUserApplications(user, 2);
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
        var dbContext = MockManager.GetDbContext();
        
        var mockUserManager = MockManager.GetMockUserManager();
        var users = mockUserManager.SetupUsers(2);
        
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);

        var applications = users
            .SelectMany(user => GenerateUserApplications(user, 2))
            .ToList();
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
        var dbContext = MockManager.GetDbContext();
        var mockUserManager = MockManager.GetMockUserManager();
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
        var dbContext = MockManager.GetDbContext();
        
        var mockUserManager = MockManager.GetMockUserManager();
        var user = mockUserManager.SetupUsers(1).Single();
        
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        
        var applications = GenerateUserApplications(user, 2);
        await dbContext.Applications.AddRangeAsync(applications);
        await dbContext.SaveChangesAsync();
        
        // Act
        var result = await applicationService.GetAllByUserAsync(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async Task GetAllByUserAsync_WithOtherUsers()
    {
        // Arrange
        var dbContext = MockManager.GetDbContext();
        
        var mockUserManager = MockManager.GetMockUserManager();
        var user = mockUserManager.SetupUsers(1).Single();
        
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);

        var applications = GenerateUserApplications(user, 2);
        
        await dbContext.Applications.AddRangeAsync(applications);
        await dbContext.SaveChangesAsync();
        
        // Act
        var result = await applicationService.GetAllByUserAsync(user.Id);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async Task GetAllByUserAsync_ReturnsNullWhenUserNotFound()
    {
        // Arrange
        var dbContext = MockManager.GetDbContext();
        
        var mockUserManager = MockManager.GetMockUserManager();
        var user = mockUserManager.SetupUsers(1).Single();
        
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        
        var applications = GenerateUserApplications(user, 2);
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
        var dbContext = MockManager.GetDbContext();
        
        var mockUserManager = MockManager.GetMockUserManager();
        var user = mockUserManager.SetupUsers(1).Single();
        
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        
        var applications = GenerateUserApplications(user, 2);
        
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
    public async Task GetByIdAsync_ReturnsNullWhenApplicationNotFound()
    {
        // Arrange
        var dbContext = MockManager.GetDbContext();
        var mockUserManager = MockManager.GetMockUserManager();
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);
        
        // Act
        var result = await applicationService.GetByIdAsync(0);
    
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task CreateAsync_ReturnsApplicationDtoOrNull()
    {
        // Arrange
        var dbContext = MockManager.GetDbContext();
        
        var mockUserManager = MockManager.GetMockUserManager();
        var user = mockUserManager.SetupUsers(1).Single();
        
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
        var result = await applicationService.CreateAsync(createRequest, user.Id);
    
        // Assert
        Assert.NotNull(result);
        Assert.Single(dbContext.Applications);
        Assert.Equal(user.Id, result.UserId);
        Assert.Equal(createRequest.Date, result.InitialDate);
    }
    
    [Fact]
    public async Task CreateAsync_ReturnsNullWhenVacantDirectionNotFound()
    {
        // Arrange
        var dbContext = MockManager.GetDbContext();
        
        var mockUserManager = MockManager.GetMockUserManager();
        var user = mockUserManager.SetupUsers(1).Single();
        
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);

        var createRequest = new CreateApplicationRequest
        {
            Type = "0",
            DetailedType = "0",
            Date = DateTime.UtcNow,
            DirectionId = 0
        };
        
        // Act
        var result = await applicationService.CreateAsync(createRequest, user.Id);
    
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task CreateAsync_ReturnsNullWhenUserNotFound()
    {
        // Arrange
        var dbContext = MockManager.GetDbContext();
        
        var mockUserManager = MockManager.GetMockUserManager();
        mockUserManager.SetupUsers(1);
        
        var applicationService = new ApplicationService(dbContext, mockUserManager.Object);

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