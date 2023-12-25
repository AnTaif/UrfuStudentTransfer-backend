namespace StudentTransfer.Utils.Dto.File;

public class FileDto
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }
    
    public int ApplicationId { get; set; }
    
    public string? Name { get; set; }
    
    public string? Extension { get; set; }
    
    public string UrlPath { get; set; } = null!; // Url path
    
    public DateTime UploadDate { get; set; }
}

public record UploadFileRequest(string FileName, int ApplicationId, Stream Stream);

public record GetFileResponse(string FileName, Stream Stream);