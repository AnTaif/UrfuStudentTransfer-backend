using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Bll.Services.Application;
using StudentTransfer.Bll.Services.File;
using StudentTransfer.Utils.Dto.Application;
using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Api.Controllers;

[ApiController]
[Route("api/applications")]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _service;
    private readonly IFileService _fileService;

    public ApplicationController(IApplicationService service, IFileService fileService)
    {
        _service = service;
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<List<ApplicationDto>> GetAll()
    {
        return await _service.GetAllAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddApplication([FromForm]CreateApplicationRequest applicationRequest, List<IFormFile> formFiles)
    {
        
        var dto = await _service.CreateAsync(applicationRequest);
        
        var fileRequests = formFiles
            .Select(formFile => new UploadFileRequest(formFile.FileName, dto.Id, formFile.OpenReadStream())).ToList();

        var fileDtos = await _fileService.UploadFileAsync(fileRequests);
        dto.Files = fileDtos;

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
            foreach (var file in files)
            {
                await _fileService.DeleteAsync(file.Id);
            }
            

        if (success)
            return NoContent();
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

    [HttpPost("{applicationId}/files")]
    public async Task<IActionResult> UploadApplicationFile(List<IFormFile> formFiles, int applicationId)
    {
        var fileRequests = formFiles
            .Select(formFile => new UploadFileRequest(formFile.FileName, applicationId, formFile.OpenReadStream())).ToList();

        var fileDtos = await _fileService.UploadFileAsync(fileRequests);

        if (fileDtos.Count == 0)
            return NotFound();
        
        return CreatedAtAction("UploadApplicationFile", fileDtos);
    }

    [HttpDelete("{applicationId}/files/{id}")]
    public async Task<IActionResult> DeleteApplicationFileById(int applicationId, Guid id)
    {
        var fileDto = await _fileService.DeleteAsync(id);

        if (fileDto == null)
            return NotFound();
        return Ok(fileDto);
    }

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