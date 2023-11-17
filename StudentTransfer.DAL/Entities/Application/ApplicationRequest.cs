using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Entities.Enums;

namespace StudentTransfer.Dal.Entities.ApplicationRequest;

public class ApplicationRequest : Entity
{
    public ApplicationType Type { get; set; }
    
    public int UserId { get; set; }
    //public virtual User User { get; set; }
    
    public int StatusId { get; set; }
    public ApplicationStatus? Status { get; set; }
    
    public List<FileRecord>? Files { get; set; }
    
    public DateTime InitialDate { get; set; }
    
    public int DirectionId { get; set; }
    public Direction? Direction { get; set; }
    
    public bool IsActive { get; set; }
}