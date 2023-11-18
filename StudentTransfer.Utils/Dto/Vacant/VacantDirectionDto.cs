namespace StudentTransfer.Utils.Dto.Vacant;

public class VacantDirectionDto
{
    public int Id { get; set; }
    
    public string Code { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public string? Level { get; set; }
    
    public int Course { get; set; }
    
    public string? Form { get; set; }
    
    public int FederalBudgets { get; set; }
    
    public int SubjectsBudgets { get; set; }
    
    public int LocalBudgets { get; set; }     
    
    public int Contracts { get; set; }
}