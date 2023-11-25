using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Utils.Dto;
using StudentTransfer.Utils.Dto.Application;
using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Bll.Mappers.Application;

public static class FileMapper
{
    public static FileDto ToDto(this FileEntity fileEntity)
    {
        return new FileDto()
        {
            Id = fileEntity.Id,
            Name = fileEntity.Name,
            Extension = fileEntity.Extension,
            Path = $"uploads/{fileEntity.Id}{fileEntity.Extension}",
            UploadDate = fileEntity.UploadTime
        };
    }

    public static FileEntity ToEntity(this FileDto dto)
    {
        return new FileEntity()
        {
            Id = dto.Id,
            Name = dto.Name,
            Extension = dto.Extension,
            Path = dto.Path,
            UploadTime = dto.UploadDate
        };
    }
}