using StudentTransfer.Dal.Entities.ApplicationRequest;
using StudentTransfer.Dal.Entities.Enums;

namespace StudentTransfer.Dal.Entities.Application;

public class ApplicationRequest : Entity
{
    public ApplicationType Type { get; set; }
    
    public int UserId { get; set; }
    //public virtual User User { get; set; }
    
    public Status CurrentStatus { get; set; }
    
    public virtual List<ApplicationStatus>? Updates { get; set; }
    
    public List<FileRecord>? Files { get; set; }
    
    public DateTime InitialDate { get; set; }
    
    public int DirectionId { get; set; }
    public Direction? Direction { get; set; }
    
    public bool IsActive { get; set; }
}