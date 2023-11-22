using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Entities.Enums;
using StudentTransfer.Utils.Dto.Application;

namespace StudentTransfer.Bll.Mappers.Application;

public static class DirectionMapper
{
    public static DirectionDto ToDto(this Direction direction)
    {
        return new DirectionDto()
        {
            Id = direction.Id,
            Code = direction.Code,
            Name = direction.Name,
            Level = direction.Level.MapToString(),
            Course = direction.Course,
            Form = direction.Form.MapToString()
        };
    }

    public static Direction ToEntity(this DirectionDto dto)
    {
        return new Direction()
        {
            Id = dto.Id,
            Code = dto.Code,
            Name = dto.Name,
            Level = dto.Level!.MapToLevel(),
            Course = dto.Course,
            Form = dto.Form!.MapToForm()
        };
    }
}