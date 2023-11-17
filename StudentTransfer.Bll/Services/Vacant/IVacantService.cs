using StudentTransfer.Dal.Entities.Enums;
using StudentTransfer.Dal.Entities.Vacant;

namespace StudentTransfer.Bll.Services.Vacant;

public interface IVacantService
{
    Task<List<VacantDirection>> GetAllAsync();

    Task<VacantDirection?> GetByIdAsync(int id);

    Task<List<VacantDirection>> GetByLevelAsync(EducationLevel level);

    Task<List<VacantDirection>> GetByFormAsync(EducationForm form);
    
    Task UpdateParseAsync();
}