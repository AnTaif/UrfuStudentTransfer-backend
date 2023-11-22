namespace StudentTransfer.Dal.Entities.Enums;

public enum Status
{
    Sent = 0,
    InProgress = 1,    
    Accepted = 2,
    Rejected = 3
}

public static class StatusMapper
{
    public static Status MapToStatus(this string strStatus)
    {
        return strStatus.ToLower() switch
        {
            ("отправлено") => Status.Sent,
            ("на рассмотрении") => Status.InProgress,
            ("принято") => Status.Accepted,
            ("отклонено") => Status.Rejected,
            _ => throw new ArgumentException("Wrong Status string argument")
        };
    }

    public static string MapToString(this Status status)
    {
        return status switch
        {
            Status.Sent => "Отправлено",
            Status.InProgress => "На рассмотрении",
            Status.Accepted => "Принято",
            Status.Rejected => "Отклонено",
            _ => throw new ArgumentException("Wrong EducationForm enum argument")
        };
    }
}