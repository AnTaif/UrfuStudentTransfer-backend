
using StudentTransfer.Utils.Dto.File;
using StudentTransfer.Utils.Dto.StatusDtos;

namespace StudentTransfer.Utils.Dto.Application;

public class ApplicationDto
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;
    
    public string DetailedType { get; set; } = null!;
    
    public Guid UserId { get; set; }

    public string UserFullName { get; set; } = null!;
    
    public string Status { get; set; } = null!;

    public List<ApplicationStatusDto> StatusUpdates { get; set; } = new();

    public DateTime InitialDate { get; set; }
    
    public DateTime UpdateDate { get; set; }

    public List<FileDto>? Files { get; set; }

    public DirectionDto Direction { get; set; } = null!;
    
    public bool IsActive { get; set; }
}

public class CreateApplicationRequest
{
    public string Type { get; set; } = null!;
    
    public string DetailedType { get; set; } = null!;
    
    public DateTime Date { get; set; }
    
    public int DirectionId { get; set; }
}

public class UpdateApplicationRequest
{
    public string? Type { get; set; }
    
    public string? Status { get; set; }

    public int? DirectionId { get; set; }
} 