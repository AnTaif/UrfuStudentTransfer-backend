using StudentTransfer.Dal.Entities.Enums;

namespace StudentTransfer.Dal.Entities.Application;

public class Direction
{
    public int Id { get; set; }
    
    public string Code { get; init; } = null!;
    
    public string Name { get; init; } = null!;

    public EducationLevel Level { get; init; }
    
    public int Course { get; init; }
    
    public EducationForm Form { get; init; }
}