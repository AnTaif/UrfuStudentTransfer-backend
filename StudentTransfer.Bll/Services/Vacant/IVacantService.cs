using StudentTransfer.Dal.Entities.Enums;
using StudentTransfer.Dal.Entities.Vacant;
using StudentTransfer.Utils.Dto.Vacant;

namespace StudentTransfer.Bll.Services.Vacant;

public interface IVacantService
{
    Task<List<VacantDirectionDto>> GetAllAsync();

    Task<VacantDirectionDto?> GetByIdAsync(int id);

    Task<List<VacantDirectionDto>> GetByLevelAsync(EducationLevel level);

    Task<List<VacantDirectionDto>> GetByFormAsync(EducationForm form);
    
    Task UpdateParseAsync();
}