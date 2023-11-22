namespace StudentTransfer.Dal.Entities.Enums;

public enum ApplicationType
{
    Recovery = 0,
    Transfer = 1
}

public static class ApplicationTypeMapper
{
    public static ApplicationType MapToApplicationType(this string strType)
    {
        return strType.ToLower() switch
        {
            ("восстановление") => ApplicationType.Recovery, 
            ("перевод") => ApplicationType.Transfer,
            _ => throw new ArgumentException("Wrong ApplicationType string argument")
        };  
    }

    public static string MapToString(this ApplicationType type)
    {
        return type switch
        {
            ApplicationType.Recovery => "Восстановление",
            ApplicationType.Transfer => "Перевод",
            _ => throw new ArgumentException("Wrong ApplicationType enum argument")
        };
    }
}