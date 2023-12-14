using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Dal.Enums;
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
            Level = direction.Level.ConvertToString(),
            Course = direction.Course,
            Form = direction.Form.ConvertToString()
        };
    }

    public static Direction ToEntity(this DirectionDto dto)
    {
        return new Direction()
        {
            Id = dto.Id,
            Code = dto.Code,
            Name = dto.Name,
            Level = dto.Level!.ConvertToLevel(),
            Course = dto.Course,
            Form = dto.Form!.ConvertToForm()
        };
    }
}