using System.ComponentModel.DataAnnotations;

namespace StudentTransfer.Utils.Dto;

public class FileDto
{
    public string? Name { get; set; }
    
    public string? Extension { get; set; }
    
    [Required]
    public string Path { get; set; } = null!;
}