using StudentTransfer.Bll.Services.Vacant;
using StudentTransfer.Dal.Entities.Vacant;
using StudentTransfer.Dal.Enums;

namespace StudentTransfer.UnitTests.Bll;

public class VacantServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllVacantDirectionDtos()
    {
        // Arrange
        var dbContext = MockManager.GetDbContext();

        var vacantService = new VacantService(dbContext);

        var vacantDirections = new List<VacantDirection>()
        {
            new()
            {
                Code = "code1",
                Name = "name1",
                Level = EducationLevel.Bachelor,
                Course = 0,
                Form = EducationForm.FullTime,
                FederalBudgets = 3,
                SubjectsBudgets = 1,
                LocalBudgets = 7,
                Contracts = 0
            },
            new()
            {
                Code = "code2",
                Name = "name2",
                Level = EducationLevel.Magistracy,
                Course = 0,
                Form = EducationForm.Mixed,
                FederalBudgets = 0,
                SubjectsBudgets = 2,
                LocalBudgets = 3,
                Contracts = 4
            }
        };

        await dbContext.VacantList.AddRangeAsync(vacantDirections);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await vacantService.GetAllAsync();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(vacantDirections.Count, result.Count);
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnEmptyWhenNoVacantDirections()
    {
        // Arrange
        var dbContext = MockManager.GetDbContext();

        var vacantService = new VacantService(dbContext);

        // Act
        var result = await vacantService.GetAllAsync();

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnsVacantDirectionDtoById()
    {
        // Arrange
        var dbContext = MockManager.GetDbContext();

        var vacantService = new VacantService(dbContext);

        var vacantDirections = new List<VacantDirection>()
        {
            new()
            {
                Code = "code1",
                Name = "name1",
                Level = EducationLevel.Bachelor,
                Course = 0,
                Form = EducationForm.FullTime,
                FederalBudgets = 3,
                SubjectsBudgets = 1,
                LocalBudgets = 7,
                Contracts = 0
            },
            new()
            {
                Code = "code2",
                Name = "name2",
                Level = EducationLevel.Magistracy,
                Course = 0,
                Form = EducationForm.Mixed,
                FederalBudgets = 0,
                SubjectsBudgets = 2,
                LocalBudgets = 3,
                Contracts = 4
            }
        };

        await dbContext.VacantList.AddRangeAsync(vacantDirections);
        await dbContext.SaveChangesAsync();

        var testDirection = dbContext.VacantList.Last();

        // Act
        var result = await vacantService.GetByIdAsync(testDirection.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testDirection.Id, result.Id);
        Assert.Equal(testDirection.Code, result.Code);
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnNullWhenNotFound()
    {
        // Arrange
        var dbContext = MockManager.GetDbContext();

        var vacantService = new VacantService(dbContext);

        // Act
        var result = await vacantService.GetByIdAsync(0);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task UpdateParseAsync_VacantListNotEmpty()
    {
        // Arrange
        var dbContext = MockManager.GetDbContext();

        var vacantService = new VacantService(dbContext);

        // Act
        await vacantService.UpdateParseAsync();

        // Assert
        Assert.NotEmpty(dbContext.VacantList.ToList());
    }
}