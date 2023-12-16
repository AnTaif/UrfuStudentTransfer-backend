using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using StudentTransfer.Bll.Services.Application;
using StudentTransfer.Bll.Services.File;
using StudentTransfer.Utils;
using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Api.Controllers;

[Authorize]
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
    public async Task<ActionResult<List<FileDto>>> GetAllByApplicationId(int applicationId)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        var isAdmin = User.IsInRole(RoleConstants.Admin);

        if (userId == null)
            return Unauthorized();
        
        var application = await _applicationService.GetByIdAsync(applicationId);
        if (application == null)
            return NotFound();

        if (application.UserId != Guid.Parse(userId) && !isAdmin)
            return Forbid();
        
        var dtos = await _fileService.GetAllByApplicationAsync(applicationId);

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FileDto>> GetById(Guid id, int applicationId)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        var isAdmin = User.IsInRole(RoleConstants.Admin);

        if (userId == null)
            return Unauthorized();
        
        var fileDto = await _fileService.GetFileDtoAsync(id);

        if (fileDto == null || fileDto.ApplicationId != applicationId)
            return NotFound();

        if (fileDto.OwnerId != Guid.Parse(userId) && !isAdmin)
            return Forbid();

        var filePath = fileDto.UrlPath;
        
        return PhysicalFile(filePath, "application/octet-stream", fileDto.Name);
    }

    [HttpPost]
    public async Task<ActionResult<List<FileDto>>> UploadApplicationFiles(int applicationId, List<IFormFile> formFiles)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        var isAdmin = User.IsInRole(RoleConstants.Admin);
        
        if (userId == null)
            return Unauthorized();
        
        var application = await _applicationService.GetByIdAsync(applicationId);
        if (application == null)
            return NotFound();

        if (application.UserId != Guid.Parse(userId) && !isAdmin)
            return Forbid();
        
        var fileRequests = formFiles
            .Select(formFile => new UploadFileRequest(formFile.FileName, applicationId, formFile.OpenReadStream())).ToList();
        
        var fileDtos = await _fileService.UploadAsync(fileRequests, application.UserId);
    
        if (fileDtos.Count == 0)
            return NotFound();
        
        return CreatedAtAction("UploadApplicationFiles", fileDtos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplicationFileById(Guid id, int applicationId)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid);
        var isAdmin = User.IsInRole(RoleConstants.Admin);

        if (userId == null)
            return Unauthorized();

        var fileDto = await _fileService.GetFileDtoAsync(id);

        if (fileDto == null || fileDto.ApplicationId != applicationId)
            return NotFound();

        if (fileDto.OwnerId != Guid.Parse(userId) && !isAdmin)
            return Forbid();
        
        var success = await _fileService.TryDeleteAsync(id);

        if (success)
            return NoContent();
        return NotFound();
    }
}