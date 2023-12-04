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

        var fileDtos = await _fileService.UploadAsync(fileRequests);
        dto.Files = fileDtos;

        return CreatedAtAction("AddApplication", dto);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateApplication(UpdateApplicationRequest request, int id)
    {
        var success = await _service.TryUpdateAsync(id, request);

        if (success)
            return NoContent();
        return NotFound();
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
                await _fileService.TryDeleteAsync(file.Id);
            }
        
        if (success)
            return NoContent();
        return NotFound();
    }
}