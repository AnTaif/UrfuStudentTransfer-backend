using System.ComponentModel.DataAnnotations;

namespace StudentTransfer.Utils.Dto.StatusDtos;

public class UpdateStatusRequest
{
    [Required]
    public int ApplicationId { get; set; }
    
    [Required]
    public string Status { get; set; } = null!;
    
    [MaxLength(255)]
    public string? Comment { get; set; }
}
