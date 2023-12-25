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
            OwnerId = fileEntity.AppUserId,
            Name = fileEntity.Name,
            ApplicationId = fileEntity.ApplicationEntityId,
            Extension = fileEntity.Extension,
            
            //TODO: remove hardcoded  localhost
            UrlPath = $"http://localhost:8080/uploads/{fileEntity.Id}{fileEntity.Extension}", // Url to the file
            
            UploadDate = fileEntity.UploadTime
        };
    }

    public static FileEntity ToEntity(this FileDto dto)
    {
        return new FileEntity()
        {
            Id = dto.Id,
            AppUserId = dto.OwnerId,
            Name = dto.Name,
            Extension = dto.Extension,
            ApplicationEntityId = dto.ApplicationId,
            Path = dto.UrlPath, // In case of storing files locally, the UrlPath is the local path to the file      
            UploadTime = dto.UploadDate
        };
    }
}