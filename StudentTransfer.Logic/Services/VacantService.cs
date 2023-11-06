using Microsoft.EntityFrameworkCore;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Vacant;

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

    public async Task CreateAsync(EducationDirection direction)
    {
        _dbContext.VacantList.Add(direction);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var data = await _dbContext.VacantList.FindAsync(id);
        if (data != null)
        {
            _dbContext.VacantList.Remove(data);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAllDataAsync()
    {
        _dbContext.VacantList.RemoveRange(_dbContext.VacantList);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddEnumerableAsync(IEnumerable<EducationDirection> directions)
    {
        _dbContext.VacantList.AddRange(directions);
        await _dbContext.SaveChangesAsync();
    }
}