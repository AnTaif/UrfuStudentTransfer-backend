using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Bll.Services.File;

public interface IFileService
{
    public Task<List<FileDto>> UploadFileAsync(List<UploadFileRequest> fileRequests);

    public Task<FileDto> GetFileAsync(Guid id);

    public List<FileDto> Delete(List<FileDto> fileDtos);
    
}