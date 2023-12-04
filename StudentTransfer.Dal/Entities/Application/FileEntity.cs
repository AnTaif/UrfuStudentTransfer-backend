using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTransfer.Dal.Entities.Application;

public class FileEntity
{
    [Key]
    public Guid Id { get; set; }
    
    //public Guid OwnerId { get; set; }
    
    public int ApplicationEntityId { get; set; }
    
    public string? Name { get; set; }

    public string Path { get; set; } = null!;
    
    public string? Extension { get; set; }
    
    public DateTime UploadTime { get; set; }
}