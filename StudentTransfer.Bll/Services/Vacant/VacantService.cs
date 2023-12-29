using Microsoft.EntityFrameworkCore;
using StudentTransfer.Bll.Mappers;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Vacant;
using StudentTransfer.Utils.Dto.Vacant;
using StudentTransfer.VacantParser;

namespace StudentTransfer.Bll.Services.Vacant;

public class VacantService : IVacantService
{
    private readonly StudentTransferContext _dbContext;

    public VacantService(StudentTransferContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<VacantDirectionDto>> GetAllAsync()
    {
        var directions = await _dbContext.VacantList.ToListAsync();
        var dtos = directions.Select(d => d.ToDto()).ToList();
        return dtos;
    }
    
    public async Task<VacantDirectionDto?> GetByIdAsync(int id)
    {
        var direction = await _dbContext.VacantList.FirstOrDefaultAsync(e => e.Id == id);
        return direction?.ToDto();
    }

    public async Task UpdateParseAsync()
    {
        var vacantList = await _dbContext.VacantList.ToListAsync();
        var vacantDirections = await VacantListParser.ParseVacantItemsAsync();

        if (vacantDirections != null && !AreIdentical(vacantList, vacantDirections))
        {
            await UpdateAsync(vacantDirections);
        }
    }

    private async Task UpdateAsync(IEnumerable<VacantDirection> newDirections)
    {
        await DeleteAllDataAsync();
        await AddListAsync(newDirections);
    }
    
    private async Task DeleteAllDataAsync()
    {
        _dbContext.VacantList.RemoveRange(_dbContext.VacantList);
        await _dbContext.SaveChangesAsync();
    }

    private async Task AddListAsync(IEnumerable<VacantDirection> directions)
    {
        await _dbContext.VacantList.AddRangeAsync(directions);
        await _dbContext.SaveChangesAsync();   
    }

    private static bool AreIdentical(List<VacantDirection> vacantList, List<VacantDirection> directions)
    {
        return directions.All(direction => 
            vacantList.Any(vacantDirection => 
                vacantDirection.Code == direction.Code &&
                vacantDirection.Name == direction.Name &&
                vacantDirection.Level == direction.Level &&
                vacantDirection.Course == direction.Course &&
                vacantDirection.Form == direction.Form &&
                vacantDirection.FederalBudgets == direction.FederalBudgets &&
                vacantDirection.SubjectsBudgets == direction.SubjectsBudgets &&
                vacantDirection.LocalBudgets == direction.LocalBudgets &&
                vacantDirection.Contracts == direction.Contracts)
        );
    }
}