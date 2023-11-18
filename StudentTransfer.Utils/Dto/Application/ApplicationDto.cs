namespace StudentTransfer.Utils.Dto.Application;

public class ApplicationDto
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public string Status { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public List<FileDto>? Files { get; set; }

    public DirectionDto Direction { get; set; } = null!;
    
    public bool IsActive { get; set; }
}

public record CreateApplicationRequest(
    int UserId, 
    DateTime Date, 
    int DirectionId);