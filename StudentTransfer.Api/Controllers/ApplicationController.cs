using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Bll.Services.Application;
using StudentTransfer.Utils.Dto.Application;
using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Api.Controllers;

[ApiController]
[Route("api/applications")]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _service;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ApplicationController(IApplicationService service, IWebHostEnvironment hostEnvironment)
    {
        _service = service;
        _hostEnvironment = hostEnvironment;
    }

    [HttpGet]
    public async Task<List<ApplicationDto>> GetAll()
    {
        return await _service.GetAllAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddApplication([FromForm]CreateApplicationRequest applicationRequest, List<IFormFile> formFiles)
    {
        var fileRootPath = Path.Combine(_hostEnvironment.ContentRootPath, "Uploads");
        Directory.CreateDirectory(fileRootPath);

        var fileDtos = new List<FileDto>();
        
        foreach (var formFile in formFiles)
        {
            var fileId = Guid.NewGuid();
            var extension = Path.GetExtension(formFile.FileName);
            var filePath = Path.Combine(fileRootPath, fileId + extension);

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            var fileDto = new FileDto
            {
                Id = fileId,
                OwnerId = Guid.NewGuid(),
                Name = formFile.FileName,
                Extension = extension,
                Path = filePath,
                UploadDate = DateTime.UtcNow
            };
            
            fileDtos.Add(fileDto);
        }
        
        var dto = await _service.CreateAsync(applicationRequest, fileDtos);

        return CreatedAtAction("AddApplication", dto);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateApplication(UpdateApplicationRequest request, int id)
    {
        var dto = await _service.UpdateAsync(id, request);

        if (dto == null)
            return NotFound();

        return Ok(dto);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteApplication(int id)
    {
        var applicationDto = await _service.GetByIdAsync(id);
        if (applicationDto == null)
            return NotFound();
        
        var success = await _service.TryDeleteAsync(id);
        
        var files = applicationDto.Files;
        
        if (success && files != null)
            foreach (var file in files.Where(file => System.IO.File.Exists(file.Path)))
            {
                System.IO.File.Delete(file.Path);
            }

        if (success)
            return Ok();
        return NotFound();
    }

    [HttpGet("{applicationId}/files")]
    public async Task<IActionResult> GetApplicationFiles(int applicationId)
    {
        var applicationDto = await _service.GetByIdAsync(applicationId);

        if (applicationDto == null)
            return NotFound();

        return Ok(applicationDto.Files);
    }

    // TODO: fix error http://go.microsoft.com/fwlink/?LinkId=527962
    // [HttpPost("{applicationId}/files")]
    // public async Task<IActionResult> UploadApplicationFile(List<IFormFile> formFiles, int applicationId)
    // {
    //     var fileRootPath = Path.Combine(_hostEnvironment.ContentRootPath, "Uploads");
    //     Directory.CreateDirectory(fileRootPath);
    //
    //     var fileUploadDtos = new List<FileDto>();
    //     
    //     foreach (var formFile in formFiles)
    //     {
    //         var fileId = Guid.NewGuid();
    //         var extension = Path.GetExtension(formFile.FileName);
    //         var filePath = Path.Combine(fileRootPath, fileId + extension);
    //
    //         await using (var fileStream = new FileStream(filePath, FileMode.Create))
    //         {
    //             await formFile.CopyToAsync(fileStream);
    //         }
    //
    //         var fileDto = new FileDto
    //         {
    //             Id = fileId,
    //             OwnerId = Guid.NewGuid(),
    //             Name = formFile.FileName,
    //             Extension = extension,
    //             Path = filePath,
    //             UploadDate = DateTime.UtcNow
    //         };
    //         
    //         fileUploadDtos.Add(fileDto);
    //     }
    //
    //     var fileDtos = await _service.UploadFilesAsync(applicationId, fileUploadDtos);
    //
    //     if (fileDtos == null)
    //     {
    //         return NotFound();
    //     }
    //     
    //     return CreatedAtAction("UploadApplicationFile", fileDtos);
    // }

    [HttpGet("{applicationId}/files/{id}")]
    public async Task<IActionResult> GetApplicationFileById(int applicationId, Guid id)
    {
        var applicationDto = await _service.GetByIdAsync(applicationId);
        var fileDto = applicationDto?.Files?.FirstOrDefault(f => f.Id == id);
        
        if (fileDto == null)
            return NotFound();
        
        var filePath = fileDto.Path;
        
        return PhysicalFile(filePath, "application/octet-stream", fileDto.Name);
    }
}