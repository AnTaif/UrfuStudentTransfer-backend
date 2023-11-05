namespace StudentTransfer.Dal.Models.Vacant;

public enum EducationForm
{
    FullTime,
    PartTime,
    Mixed
}

public static class EducationFormConverter
{
    public static EducationForm ConvertToForm(string strForm)
    {
        return strForm switch
        {
            ("Очная") => EducationForm.FullTime,
            ("Заочная") => EducationForm.PartTime,
            ("Очно-заочная") => EducationForm.Mixed,
            _ => throw new ArgumentException("")
        };
    }
}