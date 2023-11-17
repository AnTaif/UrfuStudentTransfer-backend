using StudentTransfer.Dal.Entities.Enums;

namespace StudentTransfer.Dal.Entities.ApplicationRequest;

public class Direction : Entity
{
    public string Code { get; init; } = null!;
    
    public string Name { get; init; } = null!;

    public EducationLevel Level { get; init; }
    
    public int Course { get; init; }
    
    public EducationForm Form { get; init; }
    
    public int FederalBudgets { get; init; }
    
    public int SubjectsBudgets { get; init; }
    
    public int LocalBudgets { get; init; }     
    
    public int Contracts { get; init; }
}