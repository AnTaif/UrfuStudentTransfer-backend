using Microsoft.EntityFrameworkCore;
using StudentTransfer.Bll.Mappers.Application;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Enums;
using StudentTransfer.Utils.Dto.StatusDtos;

namespace StudentTransfer.Bll.Services.StatusServices;

public class StatusService : IStatusService
{
    private readonly StudentTransferContext _context;

    public StatusService(StudentTransferContext context)
    {
        _context = context;
    }
    
    public async Task<ApplicationStatusDto?> GetApplicationStatusAsync(int applicationId)
    {
        var application = await _context.Applications
            .Include(applicationEntity => applicationEntity.StatusUpdates)
            .FirstOrDefaultAsync(a => a.Id == applicationId);

        return application?.StatusUpdates.LastOrDefault()?.ToDto();
    }

    public async Task<List<ApplicationStatusDto>?> GetStatusHistoryAsync(int applicationId)
    {
        var application = await _context.Applications
            .Include(applicationEntity => applicationEntity.StatusUpdates)
            .FirstOrDefaultAsync(a => a.Id == applicationId);

        var dtos = application?.StatusUpdates.Select(s => s.ToDto()).ToList(); 
        return dtos;
    }

    public async Task<bool> TryUpdateStatusAsync(UpdateStatusRequest request, int applicationId)
    {
        var application = await _context.Applications
            .Include(applicationEntity => applicationEntity.StatusUpdates)
            .FirstOrDefaultAsync(a => a.Id == applicationId);

        if (application == null)
            return false;
        
        var newStatus = new ApplicationStatus
        {
            Id = new Guid(),
            Status = request.Status.ConvertToStatus(),
            Comment = request.Comment,
            Date = DateTime.UtcNow
        };
        
        application.StatusUpdates.Add(newStatus);
        application.CurrentStatus = newStatus.Status;

        await _context.SaveChangesAsync();

        return true;
    }
}