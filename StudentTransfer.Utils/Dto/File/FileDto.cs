using Microsoft.AspNetCore.Http;

namespace StudentTransfer.Utils.Dto.File;

public class FileDto
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; } = new Guid();
    
    public int ApplicationId { get; set; }
    
    public string? Name { get; set; }
    
    public string? Extension { get; set; }
    
    public string Path { get; set; } = null!; // Переменная штука
    
    public DateTime UploadDate { get; set; }
}

public record UploadFileRequest(string FileName, int ApplicationId, Stream Stream);

public record GetPhysicalFileResponse(string FileName, Stream Stream);