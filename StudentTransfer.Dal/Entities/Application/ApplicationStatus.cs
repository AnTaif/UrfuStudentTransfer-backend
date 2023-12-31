using StudentTransfer.Dal.Enums;

namespace StudentTransfer.Dal.Entities.Application;

public class ApplicationStatus
{
    public Guid Id { get; set; }
    
    public Status Status { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime Date { get; set; }
}