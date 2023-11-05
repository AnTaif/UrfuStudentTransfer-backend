namespace StudentTransfer.Dal.Models.Vacant;

public enum EducationLevel
{
    Bachelor,
    Specialty,
    Magistracy
}

public static class EducationLevelConverter
{
    public static EducationLevel ConvertToLevel(string strLevel)
    {
        return strLevel switch
        {
            ("высшее образование - бакалавриат") => EducationLevel.Bachelor,
            ("высшее образование - специалитет") => EducationLevel.Specialty,
            ("высшее образование - магистратура") => EducationLevel.Magistracy,
            _ => throw new ArgumentException("")
        };
    }
}
