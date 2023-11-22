using Microsoft.AspNetCore.Http;

namespace StudentTransfer.Utils.Dto.Application;

public class FileDto
{
    public Guid Id { get; set; }
    
    //public Guid OwnerId { get; set; }
    
    public string? Name { get; set; }
    
    public string? Extension { get; set; }
    
    public string Path { get; set; } = null!;
    
    public DateTime UploadDate { get; set; }
}

public record UploadFileRequest(string Name, string Extension, IFormFile Stream);