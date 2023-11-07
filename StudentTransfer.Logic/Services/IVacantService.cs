using StudentTransfer.Dal.Entities.Vacant;

namespace StudentTransfer.Logic.Services;

public interface IVacantService
{
    Task<List<EducationDirection>> GetAllAsync();

    Task<EducationDirection?> GetByIdAsync(int id);

    Task<List<EducationDirection>> GetByLevelAsync(EducationLevel level);

    Task<List<EducationDirection>> GetByFormAsync(EducationForm form);

    // TODO: Should I put these methods for future???
    // Task ClearAsync();
    //
    // Task AddDirectionsAsync(IEnumerable<EducationDirection> directions);

    // TODO: Maybe separate parsing and database update code???
    Task UpdateParseAsync();
}