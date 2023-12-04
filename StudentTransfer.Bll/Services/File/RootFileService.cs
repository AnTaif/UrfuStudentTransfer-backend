using StudentTransfer.Bll.Mappers.Application;
using StudentTransfer.Dal;
using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Bll.Services.File;

public class RootFileService : IFileService
{
    private readonly StudentTransferContext _context;
    private readonly string _rootPath;
    
    public RootFileService(StudentTransferContext context, string rootPath)
    {
        _context = context;
        _rootPath = rootPath;
    }

    public async Task<List<FileDto>> UploadFileAsync(List<UploadFileRequest> fileRequests)
    {
        var fileDtos = new List<FileDto>();
        
        foreach (var fileRequest in fileRequests)
        {
            var fileId = Guid.NewGuid();
            var extension = Path.GetExtension(fileRequest.FileName);
            var filePath = Path.Combine(_rootPath, fileId + extension);

            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await fileRequest.Stream.CopyToAsync(fileStream);
            
            var fileDto = new FileDto
            {
                Id = fileId,
                OwnerId = Guid.NewGuid(),
                Name = fileRequest.FileName,
                ApplicationId = fileRequest.ApplicationId,
                Extension = extension,
                Path = filePath,
                UploadDate = DateTime.UtcNow
            };
            
            fileDtos.Add(fileDto);
        }

        var entities = fileDtos.Select(f => f.ToEntity());
        _context.Files.AddRange(entities);
        await _context.SaveChangesAsync();

        return fileDtos;
    }

    public Task<FileDto> GetFileAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public  List<FileDto> Delete(List<FileDto> fileDtos)
    {
        var deletedDtos = new List<FileDto>();
        
        foreach (var fileDto in fileDtos)
        {
            if (System.IO.File.Exists(fileDto.Path))
                System.IO.File.Delete(fileDto.Path);
            
            deletedDtos.Add(fileDto);
        }

        return deletedDtos;
    }
}