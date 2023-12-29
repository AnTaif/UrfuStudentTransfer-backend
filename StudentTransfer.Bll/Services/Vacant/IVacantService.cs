using StudentTransfer.Utils.Dto.Vacant;

namespace StudentTransfer.Bll.Services.Vacant;

public interface IVacantService
{
    Task<List<VacantDirectionDto>> GetAllAsync();

    Task<VacantDirectionDto?> GetByIdAsync(int id);
    
    Task UpdateParseAsync();
}