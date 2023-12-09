using System.ComponentModel.DataAnnotations;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Enums;

namespace StudentTransfer.Dal.Entities.Vacant;

public class VacantDirection
{
    public int Id { get; set; }
    
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