using System.Globalization;
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

    public async Task<List<FileDto>> GetAllByApplicationAsync(int applicationId)
    {
        var files = await _context.Files.Where(f => f.ApplicationEntityId == applicationId).ToListAsync();
        var dtos = files.Select(f => f.ToDto()).ToList();

        return dtos;
    }
    
    public async Task<List<FileDto>> UploadAsync(List<UploadFileRequest> fileRequests, Guid userId)
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
                OwnerId = userId,
                Name = fileRequest.FileName,
                ApplicationId = fileRequest.ApplicationId,
                Extension = extension,
                UrlPath = filePath,
                UploadDate = DateTime.UtcNow
            };
            
            fileDtos.Add(fileDto);
        }

        var entities = fileDtos.Select(f => f.ToEntity()).ToList();
        _context.Files.AddRange(entities);
        await _context.SaveChangesAsync();

        return entities.Select(f => f.ToDto()).ToList();
    }

    public async Task<GetFileResponse?> GetFileAsync(Guid id)
    {
        var file = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);

        if (file == null)
            return null;
        
        var fileName = file.Name ?? file.UploadTime.ToString(CultureInfo.InvariantCulture) + file.Extension;
        var fileStream = new FileStream(file.Path, FileMode.Open);

        return new GetFileResponse(fileName, fileStream);
    }

    public async Task<FileDto?> GetFileDtoAsync(Guid id)
    {
        var file = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);

        return file?.ToDto();
    }

    public async Task<bool> TryDeleteAsync(Guid id)
    {
        var fileEntity = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);

        if (fileEntity == null)
            return false;
        
        _context.Files.Remove(fileEntity);
        
        if (System.IO.File.Exists(fileEntity.Path))
            System.IO.File.Delete(fileEntity.Path);
        
        await _context.SaveChangesAsync();
        return true;
    }
}