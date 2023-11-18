using Microsoft.EntityFrameworkCore;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Enums;
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
        var dtos = directions.Select(d => new VacantDirectionDto()
        {
            Id = d.Id,
            Code = d.Code,
            Name = d.Name,
            Level = d.Level.MapToString(),
            Course = d.Course,
            Form = d.Form.MapToString(),
            FederalBudgets = d.FederalBudgets,
            SubjectsBudgets = d.SubjectsBudgets,
            LocalBudgets = d.LocalBudgets,
            Contracts = d.Contracts
        });
        return dtos.ToList();
        
        //TODO: переместить маппинг в дто в отдельный класс
    }
    
    public async Task<VacantDirectionDto?> GetByIdAsync(int id)
    {
        var direction = await _dbContext.VacantList.FirstOrDefaultAsync(e => e.Id == id);

        if (direction == null)
            return null;
        
        var dto = new VacantDirectionDto()
        {
            Id = direction.Id,
            Code = direction.Code,
            Name = direction.Name,
            Level = direction.Level.MapToString(),
            Course = direction.Course,
            Form = direction.Form.MapToString(),
            FederalBudgets = direction.FederalBudgets,
            SubjectsBudgets = direction.SubjectsBudgets,
            LocalBudgets = direction.LocalBudgets,
            Contracts = direction.Contracts
        };

        return dto;
    }

    public async Task<List<VacantDirectionDto>> GetByLevelAsync(EducationLevel level)
    {
        var directions = await _dbContext.VacantList.Where(e => e.Level == level).ToListAsync();
        
        var dtos = directions.Select(d => new VacantDirectionDto()
        {
            Id = d.Id,
            Code = d.Code,
            Name = d.Name,
            Level = d.Level.MapToString(),
            Course = d.Course,
            Form = d.Form.MapToString(),
            FederalBudgets = d.FederalBudgets,
            SubjectsBudgets = d.SubjectsBudgets,
            LocalBudgets = d.LocalBudgets,
            Contracts = d.Contracts
        });
        return dtos.ToList();
    }

    public async Task<List<VacantDirectionDto>> GetByFormAsync(EducationForm form)
    {
        var directions = await _dbContext.VacantList.Where(e => e.Form == form).ToListAsync();
        
        var dtos = directions.Select(d => new VacantDirectionDto()
        {
            Id = d.Id,
            Code = d.Code,
            Name = d.Name,
            Level = d.Level.MapToString(),
            Course = d.Course,
            Form = d.Form.MapToString(),
            FederalBudgets = d.FederalBudgets,
            SubjectsBudgets = d.SubjectsBudgets,
            LocalBudgets = d.LocalBudgets,
            Contracts = d.Contracts
        });
        return dtos.ToList();
    }

    public async Task UpdateParseAsync()
    {
        var vacantList = await _dbContext.VacantList.ToListAsync();
        var vacantDirections = await VacantListParser.ParseVacantItemsAsync();

        if (!AreIdentical(vacantList, vacantDirections))
        {
            await UpdateAsync(vacantDirections);
        }
    }

    private async Task UpdateAsync(List<VacantDirection> newDirections)
    {
        await DeleteAllDataAsync();
        await AddListAsync(newDirections);
    }
    
    private async Task DeleteAllDataAsync()
    {
        _dbContext.VacantList.RemoveRange(_dbContext.VacantList);
        await _dbContext.SaveChangesAsync();
    }

    private async Task AddListAsync(List<VacantDirection> directions)
    {
        _dbContext.VacantList.AddRange(directions);
        await _dbContext.SaveChangesAsync();   
    }

    private static bool AreIdentical(List<VacantDirection> vacantList, List<VacantDirection> directions)
    {
        var areIdentical = directions.All(dir => 
            vacantList.Any(e => 
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
        
        return areIdentical;
    }
}