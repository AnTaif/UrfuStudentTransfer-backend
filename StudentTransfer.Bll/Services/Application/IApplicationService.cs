using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Enums;
using StudentTransfer.Utils.Dto.Application;
using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Bll.Services.Application;

public interface IApplicationService
{
    Task<List<ApplicationDto>> GetAllAsync();
    
    Task<List<ApplicationDto>> GetActiveAsync();
    
    Task<List<ApplicationDto>> GetAllByUserAsync(Guid userId);
    
    Task<ApplicationDto?> GetByIdAsync(int id);

    Task<List<ApplicationDto>> GetByStatusAsync(Status status);
 
    Task<ApplicationDto?> CreateAsync(CreateApplicationRequest request, Guid userId);

    Task ChangeStatusAsync(int applicationId, Status newStatus);

    Task<bool> TryDeleteAsync(int id);

    Task<bool> TryUpdateAsync(int id, UpdateApplicationRequest request);
}