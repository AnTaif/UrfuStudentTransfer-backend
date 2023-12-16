using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Bll.Services.File;

public interface IFileService
{
    public Task<List<FileDto>> GetAllByApplicationAsync(int applicationId);
    
    public Task<GetFileResponse?> GetFileAsync(Guid id);

    public Task<FileDto?> GetFileDtoAsync(Guid id);
    
    public Task<bool> TryDeleteAsync(Guid id);
    
    public Task<List<FileDto>> UploadAsync(List<UploadFileRequest> fileRequests, Guid userId);
}