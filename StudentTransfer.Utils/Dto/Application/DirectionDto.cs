using System.ComponentModel.DataAnnotations;

namespace StudentTransfer.Utils.Dto.Application;

public class DirectionDto
{
    [Required]
    public string Code { get; init; } = null!;
    
    [Required]
    public string Name { get; init; } = null!;

    public string? Level { get; init; }
    
    public int Course { get; init; }

    public string? Form { get; init; }
}