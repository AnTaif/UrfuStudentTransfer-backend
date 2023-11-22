using Microsoft.AspNetCore.Http;

namespace StudentTransfer.Utils.Dto.Application;

public class ApplicationDto
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;
    
    public int UserId { get; set; }
    
    public string Status { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public List<FileDto>? Files { get; set; }

    public DirectionDto Direction { get; set; } = null!;
    
    public bool IsActive { get; set; }
}

public record CreateApplicationRequest(
    int UserId, 
    string Type,
    DateTime Date, 
    int DirectionId);