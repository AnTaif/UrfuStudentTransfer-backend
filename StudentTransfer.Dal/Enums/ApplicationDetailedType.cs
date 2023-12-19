namespace StudentTransfer.Dal.Enums;

public enum ApplicationDetailedType
{
    RecoveryToBudget = 0,
    RecoveryToContract = 1,
    ContractToBudget = 2,
    BudgetToBudgetInsideSameInstitute = 3,
    BudgetToContractInsideSameInstitute = 4,
    BudgetToBudgetDifferentInstitute = 5,
    BudgetToContractDifferentInstitute = 6
}

public static class ApplicationDetailedTypeConverter
{
    public static ApplicationDetailedType ConvertToApplicationDetailedType(this string strType)
    {
        return strType.ToLower() switch
        {
            "восстановление на бюджетную форму обучения" =>
                ApplicationDetailedType.RecoveryToBudget,
            "0" =>
                ApplicationDetailedType.RecoveryToBudget,
            
            "восстановление на контрактную форму обучения" =>
                ApplicationDetailedType.RecoveryToContract,
            "1" =>
                ApplicationDetailedType.RecoveryToContract,
            
            "перевод на бюджетную форму обучения с контрактной" =>
                ApplicationDetailedType.ContractToBudget,
            "2" =>
                ApplicationDetailedType.ContractToBudget,
            
            "перевод на другое направление внутри института с бюджета на бюджет" =>
                ApplicationDetailedType.BudgetToBudgetInsideSameInstitute,
            "3" =>
                ApplicationDetailedType.BudgetToBudgetInsideSameInstitute,
            
            "перевод на другое направление внутри института с бюджета на контракт" =>
                ApplicationDetailedType.BudgetToContractInsideSameInstitute,
            "4" =>
                ApplicationDetailedType.BudgetToContractInsideSameInstitute,
            
            "перевод на другое направление в другой институт с бюджета на бюджет" =>
                ApplicationDetailedType.BudgetToBudgetDifferentInstitute,
            "5" =>
                ApplicationDetailedType.BudgetToBudgetDifferentInstitute,
            
            "перевод на другое направление в другой институт с бюджета на контракт" =>
                ApplicationDetailedType.BudgetToContractDifferentInstitute,
            "6" =>
                ApplicationDetailedType.BudgetToContractDifferentInstitute,
            _ => throw new ArgumentException("Wrong ApplicationDetailedType string argument")
        };  
    }

    public static string ConvertToString(this ApplicationDetailedType type)
    {
        return type switch
        {
            ApplicationDetailedType.RecoveryToBudget =>
                "Восстановление на бюджетную форму обучения",
            ApplicationDetailedType.RecoveryToContract =>
                "Восстановление на контрактную форму обучения",
            ApplicationDetailedType.ContractToBudget =>
                "Перевод на бюджетную форму обучения с контрактной",
            ApplicationDetailedType.BudgetToBudgetInsideSameInstitute =>
                "Перевод на другое направление внутри института с бюджета на бюджет",
            ApplicationDetailedType.BudgetToContractInsideSameInstitute =>
                "Перевод на другое направление внутри института с бюджета на контракт",
            ApplicationDetailedType.BudgetToBudgetDifferentInstitute =>
                "Перевод на другое направление в другой институт с бюджета на бюджет",
            ApplicationDetailedType.BudgetToContractDifferentInstitute =>
                "Перевод на другое направление в другой институт с бюджета на контракт",
            _ => throw new ArgumentException("Wrong ApplicationDetailedType enum argument")
        };
    }
}