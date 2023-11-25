using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Entities.Enums;
using StudentTransfer.Utils.Dto.Application;
using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Bll.Services.Application;

public interface IApplicationService
{
    Task<List<ApplicationDto>> GetAllAsync();
    
    Task<ApplicationDto?> GetByIdAsync(int id);

    Task<ApplicationDto> CreateAsync(CreateApplicationRequest request, List<FileDto> fileDtos);

    Task<List<ApplicationDto>> GetActiveAsync();

    Task<List<ApplicationDto>> GetByStatusAsync(Status status);

    Task<bool> TryDeleteAsync(int id);

    Task<bool> TryUpdateAsync(ApplicationDto application);
}