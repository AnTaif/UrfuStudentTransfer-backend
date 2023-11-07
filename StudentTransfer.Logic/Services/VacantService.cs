using Microsoft.EntityFrameworkCore;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Vacant;
using StudentTransfer.VacantParser;

namespace StudentTransfer.Logic.Services;

public class VacantService : IVacantService
{
    private readonly StudentTransferContext _dbContext;

    public VacantService(StudentTransferContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<EducationDirection>> GetAllAsync()
    {
        return await _dbContext.VacantList.ToListAsync();
    }
    
    public async Task<EducationDirection?> GetByIdAsync(int id)
    {
        return await _dbContext.VacantList.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<EducationDirection>> GetByLevelAsync(EducationLevel level)
    {
        return await _dbContext.VacantList.Where(e => e.Level == level).ToListAsync();
    }

    public async Task<List<EducationDirection>> GetByFormAsync(EducationForm form)
    {
        return await _dbContext.VacantList.Where(e => e.Form == form).ToListAsync();
    }

    public async Task UpdateParseAsync()
    {
        var vacantDirections = await VacantListParser.ParseVacantItemsAsync();

        if (IsNewDirections(vacantDirections))
        {
            await UpdateAsync(vacantDirections);
        }
    }

    private async Task UpdateAsync(List<EducationDirection> newDirections)
    {
        await DeleteAllDataAsync();
        await AddListAsync(newDirections);
    }
    
    private async Task DeleteAllDataAsync()
    {
        _dbContext.VacantList.RemoveRange(_dbContext.VacantList);
        await _dbContext.SaveChangesAsync();
    }

    private async Task AddListAsync(List<EducationDirection> directions)
    {
        _dbContext.VacantList.AddRange(directions);
        await _dbContext.SaveChangesAsync();   
    }

    private bool IsNewDirections(IEnumerable<EducationDirection> directions)
    {
        var isNew = !directions.All(dir => 
            _dbContext.VacantList.Any(e => 
                e.Code == dir.Code &&
                e.Name == dir.Name &&
                e.Level == dir.Level &&
                e.Course == dir.Course &&
                e.Form == dir.Form &&
                e.FederalBudgets == dir.FederalBudgets &&
                e.SubjectsBudgets == dir.SubjectsBudgets &&
                e.LocalBudgets == dir.LocalBudgets &&
                e.Contracts == dir.Contracts)
        );
        
        return isNew;
    }
}