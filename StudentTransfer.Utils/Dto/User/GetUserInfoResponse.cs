namespace StudentTransfer.Utils.Dto.User;

public class GetUserInfoResponse
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;
    
    public string? PhoneNumber { get; set; }
    
    public string? Telegram { get; set; }
}