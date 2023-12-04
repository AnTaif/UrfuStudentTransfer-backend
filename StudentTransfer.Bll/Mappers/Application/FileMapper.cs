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
            ApplicationId = fileEntity.ApplicationEntityId,
            Extension = fileEntity.Extension,
            Path = $"uploads/{fileEntity.Id}{fileEntity.Extension}", // relative api path to the file
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
            ApplicationEntityId = dto.ApplicationId,
            Path = dto.Path,
            UploadTime = dto.UploadDate
        };
    }
}