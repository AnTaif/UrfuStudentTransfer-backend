using Microsoft.EntityFrameworkCore;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Entities.Enums;

namespace StudentTransfer.Bll.Services.Application;

public class ApplicationService : IApplicationService
{
    private readonly StudentTransferContext _context;

    public ApplicationService(StudentTransferContext context)
    {
        _context = context;
    }
    
    public async Task<List<ApplicationRequest>> GetAllAsync()
    {
        return await _context.Applications.ToListAsync();
    }

    public async Task<ApplicationRequest?> GetByIdAsync(int id)
    {
        return await _context.Applications.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(ApplicationRequest application)
    {
        await _context.AddAsync(application);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ApplicationRequest>> GetActiveAsync()
    {
        var activeApplications = _context.Applications.Where(a => a.IsActive);

        return await activeApplications.ToListAsync();
    }

    public async Task<List<ApplicationRequest>> GetByStatusAsync(Status status)
    {
        var application = _context.Applications.Where(a => a.CurrentStatus == status);

        return await application.ToListAsync();
    }

    public Task DeleteAsync(ApplicationRequest application)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ApplicationRequest application)
    {
        throw new NotImplementedException();
    }
}