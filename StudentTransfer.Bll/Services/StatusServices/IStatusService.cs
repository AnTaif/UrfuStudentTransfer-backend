using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Utils.Dto.StatusDtos;

namespace StudentTransfer.Bll.Services.StatusServices;

public interface IStatusService
{
    public Task<ApplicationStatusDto?> GetApplicationStatusAsync(int applicationId);
    
    public Task<List<ApplicationStatusDto>?> GetStatusHistoryAsync(int applicationId);

    public Task<bool> TryUpdateStatusAsync(UpdateStatusRequest request, int applicationId);
}
