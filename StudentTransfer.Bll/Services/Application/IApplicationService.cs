using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Entities.ApplicationRequest;
using StudentTransfer.Dal.Entities.Enums;

namespace StudentTransfer.Bll.Services.Application;

public interface IApplicationService
{
    Task<List<ApplicationRequest>> GetAllAsync();
    
    Task<ApplicationRequest?> GetByIdAsync(int id);

    Task AddAsync(ApplicationRequest application);

    Task<List<ApplicationRequest>> GetActiveAsync();

    Task<List<ApplicationRequest>> GetByStatusAsync(Status status);

    Task DeleteAsync(ApplicationRequest application);

    Task UpdateAsync(ApplicationRequest application);
}