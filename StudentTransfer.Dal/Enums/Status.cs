namespace StudentTransfer.Dal.Enums;

public enum Status
{
    Sent = 0,
    InProgress = 1,
    Accepted = 2,
    Rejected = 3,
    Closed = 4,
}

public static class StatusConverter
{
    public static Status ConvertToStatus(this string strStatus)
    {
        return strStatus.ToLower() switch
        {
            ("отправлено") => Status.Sent,
            ("на рассмотрении") => Status.InProgress,
            ("принято") => Status.Accepted,
            ("отклонено") => Status.Rejected,
            ("закрыто") => Status.Closed,
            _ => throw new ArgumentException("Wrong Status string argument")
        };
    }

    public static string ConvertToString(this Status status)
    {
        return status switch
        {
            Status.Sent => "Отправлено",
            Status.InProgress => "На рассмотрении",
            Status.Accepted => "Принято",
            Status.Rejected => "Отклонено",
            Status.Closed => "Закрыто",
            _ => throw new ArgumentException("Wrong EducationForm enum argument")
        };
    }
}