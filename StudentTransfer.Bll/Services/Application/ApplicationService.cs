using Microsoft.EntityFrameworkCore;
using StudentTransfer.Bll.Mappers.Application;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Enums;
using StudentTransfer.Utils.Dto.Application;
using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Bll.Services.Application;

public class ApplicationService : IApplicationService
{
    private readonly StudentTransferContext _context;

    public ApplicationService(StudentTransferContext context)
    {
        _context = context;
    }
    
    public async Task<List<ApplicationDto>> GetAllAsync()
    {
        var applications =  await _context.Applications
            .Include(entity => entity.Files)
            .Include(entity => entity.Direction)
            .Include(entity => entity.StatusUpdates)
            .ToListAsync();
        
        var dtos = applications.Select(a => a.ToDto()).ToList();

        return dtos;
    }

    public async Task<List<ApplicationDto>> GetAllByUserAsync(Guid userId)
    {
        var applications =  await _context.Applications
            .Where(entity => entity.AppUserId == userId)
            .Include(entity => entity.Files)
            .Include(entity => entity.Direction)
            .Include(entity => entity.StatusUpdates)
            .ToListAsync();
        
        var dtos = applications.Select(a => a.ToDto()).ToList();

        return dtos;
    }

    public async Task<ApplicationDto?> GetByIdAsync(int id)
    {
        var application = await _context.Applications
            .Include(entity => entity.Files)
            .Include(entity => entity.Direction)
            .Include(entity => entity.StatusUpdates)
            .FirstOrDefaultAsync(entity => entity.Id == id);

        var dto = application?.ToDto();

        return dto;
    }

    public async Task<ApplicationDto> CreateAsync(CreateApplicationRequest request, Guid userId)
    {
        var vacantDirection = _context.VacantList.First(v => v.Id == request.DirectionId);
        var direction = new Direction
        {
            Code = vacantDirection.Code,
            Name = vacantDirection.Name,
            Level = vacantDirection.Level,
            Course = vacantDirection.Course,
            Form = vacantDirection.Form
        };

        var newStatus = new ApplicationStatus
        {
            Id = Guid.NewGuid(),
            Status = Status.Sent,
            Comment = null,
            Date = DateTime.UtcNow
        };

        var application = new ApplicationEntity
        {
            Type = request.Type.ConvertToApplicationType(),
            DetailedType = request.DetailedType.ConvertToApplicationDetailedType(),
            AppUserId = userId,
            CurrentStatus = newStatus.Status,
            StatusUpdates = new List<ApplicationStatus>() { newStatus },
            InitialDate = request.Date.ToUniversalTime(),
            Direction = direction,
            IsActive = true
        };

        await _context.AddAsync(application);
        await _context.SaveChangesAsync();

        return application.ToDto();
    }

    public Task ChangeStatusAsync(int applicationId, Status newStatus)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ApplicationDto>> GetActiveAsync()
    {
        var activeApplications = await _context.Applications
            .Include(entity => entity.Files)
            .Include(entity => entity.Direction)
            .Include(entity => entity.StatusUpdates)
            .Where(entity => entity.IsActive)
            .ToListAsync();
        
        var dtos = activeApplications.Select(entity => entity.ToDto()).ToList();
        
        return dtos;
    }

    public async Task<List<ApplicationDto>> GetByStatusAsync(Status status)
    {
        var applications = await _context.Applications
            .Include(entity => entity.Files)
            .Include(entity => entity.Direction)
            .Include(entity => entity.StatusUpdates)
            .Where(entity => entity.CurrentStatus == status)
            .ToListAsync();

        var dtos = applications.Select(entity => entity.ToDto()).ToList();
        return dtos;
    }

    public async Task<bool> TryDeleteAsync(int id)
    {
        var application = await _context.Applications.FindAsync(id);

        if (application == null)
            return false;
            
        _context.Applications.Remove(application);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> TryUpdateAsync(int id, UpdateApplicationRequest request)
    {
        var application = await _context.Applications
            .Include(entity => entity.Files)
            .Include(entity => entity.Direction)
            .FirstOrDefaultAsync(entity => entity.Id == id);

        if (application == null)
            return false;

        if (request.Type != null)
            application.Type = request.Type.ConvertToApplicationType();

        if (request.Status != null)
            application.CurrentStatus = request.Status.ConvertToStatus();

        if (request.DirectionId != null)
        {
            var vacantDirection = await _context.VacantList
                .FirstOrDefaultAsync(v => v.Id == request.DirectionId);

            if (vacantDirection == null)
                return false;
                
            var direction = new Direction
            {
                Code = vacantDirection.Code,
                Name = vacantDirection.Name,
                Level = vacantDirection.Level,
                Course = vacantDirection.Course,
                Form = vacantDirection.Form
            };
            
            application.Direction = direction;
        }

        await _context.SaveChangesAsync();

        return true;
    }
}