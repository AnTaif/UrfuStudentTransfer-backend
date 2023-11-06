using StudentTransfer.Dal.Entities.Vacant;

namespace StudentTransfer.Logic.Services;

public interface IVacantService
{
    Task<List<EducationDirection>> GetAllAsync();

    Task<EducationDirection?> GetByIdAsync(int id);

    Task CreateAsync(EducationDirection direction);

    Task DeleteByIdAsync(int id);
    
    Task DeleteAllDataAsync();

    Task AddEnumerableAsync(IEnumerable<EducationDirection> directions);
}