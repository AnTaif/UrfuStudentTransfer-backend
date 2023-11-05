namespace StudentTransfer.Dal.Models.Vacant;

public class EducationDirection
{
    public string Code { get; set; } = null!;
    
    public string Name { get; set; } = null!;

    public EducationLevel Level { get; set; }
    
    public int Course { get; set; }
    
    public EducationForm Form { get; set; }
    
    public int FederalBudgets { get; set; }
    
    public int SubjectsBudgets { get; set; }
    
    public int LocalBudgets { get; set; }     
    
    public int Contracts { get; set; }
}