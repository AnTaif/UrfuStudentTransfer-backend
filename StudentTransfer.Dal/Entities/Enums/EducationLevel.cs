namespace StudentTransfer.Dal.Entities.Enums;

public enum EducationLevel
{
    Bachelor = 0,
    Specialty = 1,
    Magistracy = 2
}

public static class EducationLevelMapper
{
    public static EducationLevel MapToLevel(this string strLevel)
    {
        return strLevel.ToLower() switch
        {
            ("высшее образование - бакалавриат") => EducationLevel.Bachelor,
            ("бакалавриат") => EducationLevel.Bachelor,
            ("высшее образование - специалитет") => EducationLevel.Specialty,
            ("специалитет") => EducationLevel.Specialty,
            ("высшее образование - магистратура") => EducationLevel.Magistracy,
            ("магистратура") => EducationLevel.Magistracy,
            _ => throw new ArgumentException("Wrong EducationLevel string argument")
        };  
    }

    public static string MapToString(this EducationLevel level)
    {
        return level switch
        {
            EducationLevel.Bachelor => "Бакалавриат",
            EducationLevel.Specialty => "Специалитет",
            EducationLevel.Magistracy => "Магистратура",
            _ => throw new ArgumentException("Wrong EducationLevel enum argument")
        };
    }
}
