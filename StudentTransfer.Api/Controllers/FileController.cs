using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Bll.Services.Application;
using StudentTransfer.Bll.Services.File;
using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Api.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/files")]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IApplicationService _applicationService;

    public FileController(IFileService fileService, IApplicationService applicationService)
    {
        _fileService = fileService;
        _applicationService = applicationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByApplicationId(int applicationId)
    {
        // Validation
        var application = await _applicationService.GetByIdAsync(applicationId);
        if (application == null)
            return NotFound();
        
        var dtos = await _fileService.GetAllByApplicationAsync(applicationId);

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var fileDto = await _fileService.GetFileDtoAsync(id);

        if (fileDto == null)
            return NotFound();

        var filePath = fileDto.UrlPath;
        
        return PhysicalFile(filePath, "application/octet-stream", fileDto.Name);
    }

    [HttpPost]
    public async Task<IActionResult> UploadApplicationFile(int applicationId, List<IFormFile> formFiles)
    {
        // Validation
        var application = await _applicationService.GetByIdAsync(applicationId);
        if (application == null)
            return NotFound();
        
        var fileRequests = formFiles
            .Select(formFile => new UploadFileRequest(formFile.FileName, applicationId, formFile.OpenReadStream())).ToList();
        
        var fileDtos = await _fileService.UploadAsync(fileRequests);

        if (fileDtos.Count == 0)
            return NotFound();
        
        return CreatedAtAction("UploadApplicationFile", fileDtos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplicationFileById(Guid id)
    {
        var success = await _fileService.TryDeleteAsync(id);

        if (success)
            return NoContent();
        return NotFound();
    }
}