using StudentTransfer.Dal.Entities.Vacant;
using StudentTransfer.Dal.Enums;
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