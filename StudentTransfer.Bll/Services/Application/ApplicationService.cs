using Microsoft.EntityFrameworkCore;
using StudentTransfer.Bll.Mappers.Application;
using StudentTransfer.Dal;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Entities.Enums;
using StudentTransfer.Utils.Dto.Application;
using File = System.IO.File;

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
        var applications =  await _context.Applications.Include(a => a.Direction).ToListAsync();
        var dtos = applications.Select(a => a.ToDto()).ToList();

        return dtos;
    }

    public async Task<ApplicationDto?> GetByIdAsync(int id)
    {
        var application = await _context.Applications.FirstOrDefaultAsync(a => a.Id == id);

        var dto = application?.ToDto();

        return dto;
    }

    public async Task<ApplicationDto> CreateAsync(CreateApplicationRequest request, List<FileDto> fileDtos)
    {
        //TODO: Save files

        var files = fileDtos.Select(f => f.ToEntity()).ToList();

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
            Files = files,
            InitialDate = request.Date.ToUniversalTime(),
            Direction = direction,
            IsActive = true
        };

        await _context.AddAsync(application);
        await _context.SaveChangesAsync();

        return application.ToDto();
    }

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

    public Task DeleteAsync(ApplicationDto application)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ApplicationDto application)
    {
        throw new NotImplementedException();
    }
}