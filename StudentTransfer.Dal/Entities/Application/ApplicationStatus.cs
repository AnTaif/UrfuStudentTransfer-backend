using StudentTransfer.Dal.Enums;

namespace StudentTransfer.Dal.Entities.Application;

public class ApplicationStatus
{
    public int Id { get; set; }
    
    public Status Status { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime UpdateDate { get; set; }
}