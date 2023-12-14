using StudentTransfer.Dal.Enums;

namespace StudentTransfer.Dal.Entities.Application;

public class Direction
{
    public int Id { get; set; }
    
    public string Code { get; set; } = null!;
    
    public string Name { get; set; } = null!;

    public EducationLevel Level { get; set; }
    
    public int Course { get; set; }
    
    public EducationForm Form { get; set; }
}