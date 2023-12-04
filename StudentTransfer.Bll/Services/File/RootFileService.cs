using Microsoft.EntityFrameworkCore;
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

    public async Task<FileDto?> GetFileAsync(Guid id)
    {
        var file = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);

        var fileDto = file?.ToDto();
        return fileDto;
    }

    public async Task<FileDto?> DeleteAsync(Guid id)
    {
        var fileEntity = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);

        if (fileEntity == null)
            return null;
        
        _context.Files.Remove(fileEntity);
        
        if (System.IO.File.Exists(fileEntity.Path))
            System.IO.File.Delete(fileEntity.Path);
        
        await _context.SaveChangesAsync();
        return fileEntity.ToDto();
    }
}