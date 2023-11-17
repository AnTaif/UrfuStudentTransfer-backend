namespace StudentTransfer.Dal.Entities.Application;

public class FileRecord : Entity
{
    public string? Name { get; set; }

    public string Path { get; set; } = null!;
    
    public string Extension { get; set; }
    
    public DateTime UploadTime { get; set; }
}