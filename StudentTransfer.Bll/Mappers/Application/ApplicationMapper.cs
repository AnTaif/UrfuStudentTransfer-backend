using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Enums;
using StudentTransfer.Utils.Dto.Application;

namespace StudentTransfer.Bll.Mappers.Application;

public static class ApplicationMapper
{
    public static ApplicationDto ToDto(this ApplicationEntity application)
    {
        var fullName = "";
        try
        {
            var user = application.User;
            fullName = $"{user.LastName} {user.FirstName}";
            if (user.MiddleName != null)
                fullName += $" {user.MiddleName}";
        }
        catch (Exception e)
        {
            // ignored
        }

        return new ApplicationDto
        {
            Id = application.Id,
            UserId = application.AppUserId,
            UserFullName = fullName,
            Type = application.Type.ConvertToString(),
            DetailedType = application.DetailedType.ConvertToString(),
            Status = application.CurrentStatus.ConvertToString(),
            StatusUpdates = application.StatusUpdates.Select(s => s.ToDto()).ToList(),
            InitialDate = application.InitialDate,
            UpdateDate = application.StatusUpdates?.LastOrDefault()?.Date ?? application.InitialDate,
            Files = application.Files?.Select(f => f.ToDto()).ToList(),
            Direction = application.Direction.ToDto(),
            IsActive = application.IsActive
        };
    }
}