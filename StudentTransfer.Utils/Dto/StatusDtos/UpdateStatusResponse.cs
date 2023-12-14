namespace StudentTransfer.Utils.Dto.StatusDtos;

public class UpdateStatusResponse
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public string? Comment { get; set; }
    
    public DateTime Date { get; set; }
}
