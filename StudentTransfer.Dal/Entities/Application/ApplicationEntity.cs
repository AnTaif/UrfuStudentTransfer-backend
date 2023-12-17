using StudentTransfer.Dal.Enums;

namespace StudentTransfer.Dal.Entities.Application;

public class ApplicationEntity
{
    public int Id { get; set; }
    
    public ApplicationType Type { get; set; }
    
    public ApplicationDetailedType DetailedType { get; set; }
    
    public Guid AppUserId { get; set; }
    //public virtual AppUser User { get; set; }
    
    public Status CurrentStatus { get; set; }
    
    public virtual List<ApplicationStatus>? Updates { get; set; }
    
    public List<FileEntity>? Files { get; set; }

    public DateTime InitialDate { get; set; }
    
    public int DirectionId { get; set; }
    public virtual Direction Direction { get; set; } = null!;

    public bool IsActive { get; set; }
}