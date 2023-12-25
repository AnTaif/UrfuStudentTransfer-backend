using StudentTransfer.Utils.Dto.StatusDtos;

namespace StudentTransfer.Bll.Services.StatusServices;

public interface IStatusService
{
    public Task<NextStatusResponse> NextStatusAsync(int id);

    public Task<UpdateStatusResponse> UpdateStatusAsync(UpdateStatusRequest request);
    
    
}
