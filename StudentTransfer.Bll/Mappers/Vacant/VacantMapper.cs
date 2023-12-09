using StudentTransfer.Dal.Entities.Vacant;
using StudentTransfer.Dal.Enums;
using StudentTransfer.Utils.Dto.Vacant;

namespace StudentTransfer.Bll.Mappers;

public static class VacantMapper
{
    public static VacantDirectionDto ToDto(this VacantDirection entity)
    {
        return new VacantDirectionDto()
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name,
            Level = entity.Level.MapToString(),
            Course = entity.Course,
            Form = entity.Form.MapToString(),
            FederalBudgets = entity.FederalBudgets,
            SubjectsBudgets = entity.SubjectsBudgets,
            LocalBudgets = entity.LocalBudgets,
            Contracts = entity.Contracts
        };
    }

    public static VacantDirection ToEntity(this VacantDirectionDto dto)
    {
        return new VacantDirection()
        {
            Id = dto.Id,
            Code = dto.Code,
            Name = dto.Name,
            Level = dto.Level!.MapToLevel(),
            Course = dto.Course,
            Form = dto.Form!.MapToForm(),
            FederalBudgets = dto.FederalBudgets,
            SubjectsBudgets = dto.SubjectsBudgets,
            LocalBudgets = dto.LocalBudgets,
            Contracts = dto.Contracts
        };
    }
}