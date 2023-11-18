using StudentTransfer.Dal.Entities.Enums;

namespace StudentTransfer.Dal.Entities.ApplicationRequest;

public class ApplicationStatus : Entity
{
    public Status Status { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime UpdateDate { get; set; }
}