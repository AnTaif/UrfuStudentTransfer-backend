using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Enums;
using StudentTransfer.Utils.Dto.Application;

namespace StudentTransfer.Bll.Mappers.Application;

public static class ApplicationMapper
{
    public static ApplicationDto ToDto(this ApplicationEntity application)
    {
        return new ApplicationDto
        {
            Id = application.Id,
            UserId = application.AppUserId,
            Type = application.Type.MapToString(),
            Status = application.CurrentStatus.MapToString(),
            UpdateDate = application.Updates?.LastOrDefault()?.UpdateDate ?? application.InitialDate,
            Files = application.Files?.Select(f => f.ToDto()).ToList(),
            Direction = application.Direction.ToDto(),
            IsActive = application.IsActive
        };
    }
}