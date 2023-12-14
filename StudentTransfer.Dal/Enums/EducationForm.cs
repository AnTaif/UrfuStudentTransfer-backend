namespace StudentTransfer.Dal.Enums;

public enum EducationForm
{
    FullTime = 0,
    PartTime = 1,
    Mixed = 2
}

public static class EducationFormConverter
{
    public static EducationForm ConvertToForm(this string strForm)
    {
        return strForm.ToLower() switch
        {
            ("очная") => EducationForm.FullTime,
            ("заочная") => EducationForm.PartTime,
            ("очно-заочная") => EducationForm.Mixed,
            _ => throw new ArgumentException("Wrong EducationForm string argument")
        };
    }

    public static string ConvertToString(this EducationForm form)
    {
        return form switch
        {
            EducationForm.FullTime => "Очная",
            EducationForm.PartTime => "Заочная",
            EducationForm.Mixed => "Очно-заочная",
            _ => throw new ArgumentException("Wrong EducationForm enum argument")
        };
    }
}