using Microsoft.EntityFrameworkCore;
using StudentTransfer.Bll.Services.StatusServices;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Enums;
using StudentTransfer.Utils.Dto.StatusDtos;
using static StudentTransfer.UnitTests.MockDataGenerator;
using static StudentTransfer.UnitTests.MockManager;

namespace StudentTransfer.UnitTests.Bll;

public class StatusServiceTests
{
    [Fact]
    public async Task GetApplicationStatusAsync_ReturnsCurrentStatus()
    {
        // Arrange
        var dbContext = GetDbContext();
        var mockUserManager = GetMockUserManager();
        
        var user = mockUserManager.GenerateAndSetupUsers(1).Single();
        
        var statusService = new StatusService(dbContext);
        
        var applications = GenerateUserApplications(user, 2);
        await dbContext.Applications.AddRangeAsync(applications);
        await dbContext.SaveChangesAsync();
        
        var testApplication = await dbContext.Applications
            .Include(applicationEntity => applicationEntity.StatusUpdates)
            .LastAsync();

        var testStatus = testApplication.StatusUpdates.Last();
        
        // Act
        var result = await statusService.GetApplicationStatusAsync(testApplication.Id);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(testStatus.Id, result.Id);
        Assert.Equal(testStatus.Date, result.Date);
    }
    
    [Fact]
    public async Task GetApplicationStatusAsync_ReturnsNullWhenNotFound()
    {
        // Arrange
        var dbContext = GetDbContext();
        var statusService = new StatusService(dbContext);
        
        // Act
        var result = await statusService.GetApplicationStatusAsync(0);
    
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetStatusHistoryAsync_ReturnsApplicationStatusHistory()
    {
        // Arrange
        var dbContext = GetDbContext();
        var statusService = new StatusService(dbContext);
        
        var mockUserManager = GetMockUserManager();
        var user = mockUserManager.GenerateAndSetupUsers(1).Single();
        
        var applications = GenerateUserApplications(user, 2);
        applications.Last().StatusUpdates.Add(new ApplicationStatus
        {
            Status = Status.InProgress,
            Comment = "123",
            Date = DateTime.UtcNow
        });
        await dbContext.Applications.AddRangeAsync(applications);
        await dbContext.SaveChangesAsync();
        var testApplication = await dbContext.Applications
            .Include(applicationEntity => applicationEntity.StatusUpdates)
            .LastAsync();
        
        // Act
        var result = await statusService.GetStatusHistoryAsync(testApplication.Id);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(testApplication.StatusUpdates.Count, result.Count);
    }
    
    [Fact]
    public async Task GetStatusHistoryAsync_ReturnsNullWhenNotFound()
    {
        // Arrange
        var dbContext = GetDbContext();
        var statusService = new StatusService(dbContext);
        
        // Act
        var result = await statusService.GetStatusHistoryAsync(0);
    
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task TryUpdateStatusAsync_ReturnsTrueWhenUpdateStatus()
    {
        // Arrange
        var dbContext = GetDbContext();
        var statusService = new StatusService(dbContext);
        
        var mockUserManager = GetMockUserManager();
        var user = mockUserManager.GenerateAndSetupUsers(1).Single();
        
        var applications = GenerateUserApplications(user, 2);
        await dbContext.Applications.AddRangeAsync(applications);
        await dbContext.SaveChangesAsync();
        var testApplication = await dbContext.Applications
            .LastAsync();

        var request = new UpdateStatusRequest
        {
            Status = Status.InProgress.ConvertToString(),
            Comment = "123"
        };

        // Act
        var result = await statusService.TryUpdateStatusAsync(request, testApplication.Id);
        var resultApplication = await dbContext.Applications
            .Include(applicationEntity => applicationEntity.StatusUpdates)
            .FirstAsync(e => e.Id == testApplication.Id);
        
        // Assert
        Assert.True(result);
        Assert.Equal(request.Status.ConvertToStatus(), resultApplication.CurrentStatus);
        Assert.Equal(2, resultApplication.StatusUpdates.Count);
    }
    
    [Fact]
    public async Task TryUpdateStatusAsync_ReturnsFalseWhenApplicationNotFound()
    {
        // Arrange
        var dbContext = GetDbContext();
        var statusService = new StatusService(dbContext);

        var request = new UpdateStatusRequest
        {
            Status = Status.InProgress.ConvertToString(),
            Comment = "123"
        };

        // Act
        var result = await statusService.TryUpdateStatusAsync(request, 0);
        
        // Assert
        Assert.False(result);
    }
}