namespace StudentTransfer.Utils.Dto.User;

public class ChangeUserInfoRequest
{
    public string? Email { get; set; } //TODO: Move email to another request with password checking
    
    public string? FullName { get; set; }

    //TODO: Add phone validation attribute
    public string? PhoneNumber { get; set; }
    
    public string? Telegram { get; set; }
}