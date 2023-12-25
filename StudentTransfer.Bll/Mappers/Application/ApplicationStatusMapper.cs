using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Enums;
using StudentTransfer.Utils.Dto.StatusDtos;

namespace StudentTransfer.Bll.Mappers.Application;

public static class ApplicationStatusMapper
{
    public static ApplicationStatusDto ToDto(this ApplicationStatus entity)
    {
        return new ApplicationStatusDto
        {
            Id = entity.Id,
            Status = entity.Status.ConvertToString(),
            Comment = entity.Comment,
            Date = entity.Date
        };
    }

    public static ApplicationStatus ToEntity(this ApplicationStatusDto dto)
    {
        return new ApplicationStatus
        {
            Id = dto.Id,
            Status = dto.Status.ConvertToStatus(),
            Comment = dto.Comment,
            Date = DateTime.UtcNow
        };
    }
}