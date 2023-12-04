using Microsoft.EntityFrameworkCore;
using StudentTransfer.Bll.Mappers.Application;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Entities.Enums;
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
        var applications =  await _context.Applications.Include(a => a.Files).Include(a => a.Direction).ToListAsync();
        var dtos = applications.Select(a => a.ToDto()).ToList();

        return dtos;
    }

    public async Task<ApplicationDto?> GetByIdAsync(int id)
    {
        var application = await _context.Applications
            .Include(a => a.Files)
            .Include(a => a.Direction)
            .FirstOrDefaultAsync(a => a.Id == id);

        var dto = application?.ToDto();

        return dto;
    }

    public async Task<ApplicationDto> CreateAsync(CreateApplicationRequest request)
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
        
        var application = new ApplicationEntity
        {
            Type = request.Type.MapToApplicationType(),
            UserId = request.UserId,
            CurrentStatus = Status.Sent,
            Updates = null,
            InitialDate = request.Date.ToUniversalTime(),
            Direction = direction,
            IsActive = true
        };

        await _context.AddAsync(application);
        await _context.SaveChangesAsync();

        return application.ToDto();
    }

    public async Task<ApplicationDto?> UpdateAsync(int id, UpdateApplicationRequest request)
    {
        var application = await _context.Applications.Include(a => a.Files).Include(a => a.Direction).FirstOrDefaultAsync(a => a.Id == id);

        if (application == null)
            return null;

        if (request.Type != null)
            application.Type = request.Type.MapToApplicationType();

        if (request.Status != null)
            application.CurrentStatus = request.Status.MapToStatus();

        if (request.DirectionId != null)
        {
            var vacantDirection = await _context.VacantList.FirstOrDefaultAsync(v => v.Id == request.DirectionId);

            if (vacantDirection == null)
                return null;
                
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

        return application.ToDto();
    }

    public Task ChangeStatusAsync(int applicationId, Status newStatus)
    {
        throw new NotImplementedException();
    }

    // TODO: fix error http://go.microsoft.com/fwlink/?LinkId=527962
    // public async Task<List<FileDto>?> UploadFilesAsync(int applicationId, List<FileDto> fileDtos)
    // {
    //     var application = await _context.Applications.Include(applicationEntity => applicationEntity.Files).FirstOrDefaultAsync(a => a.Id == applicationId);
    //     if (application == null)
    //         return null;
    //     
    //     var files = fileDtos.Select(f => f.ToEntity()).ToList();
    //
    //     foreach (var file in files)
    //     {
    //         application.Files?.Add(file);   
    //     }
    //
    //     await _context.SaveChangesAsync(); // TODO: fix error http://go.microsoft.com/fwlink/?LinkId=527962
    //     return files.Select(f => f.ToDto()).ToList();
    // }

    public async Task<List<ApplicationDto>> GetActiveAsync()
    {
        var activeApplications = await _context.Applications.Where(a => a.IsActive).ToListAsync();
        var dtos = activeApplications.Select(a => a.ToDto()).ToList();
        
        return dtos;
    }

    public async Task<List<ApplicationDto>> GetByStatusAsync(Status status)
    {
        var applications = await _context.Applications.Where(a => a.CurrentStatus == status).ToListAsync();

        var dtos = applications.Select(a => a.ToDto()).ToList();
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

    public async Task<bool> TryUpdateAsync(ApplicationDto applicationDto)
    {
        var application = await _context.Applications.FirstOrDefaultAsync(a => a.Id == applicationDto.Id);
        throw new NotImplementedException();
    }
}