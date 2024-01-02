using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using StudentTransfer.Bll.Services.Application;
using StudentTransfer.Bll.Services.File;
using StudentTransfer.Utils;
using StudentTransfer.Utils.Dto.Application;
using StudentTransfer.Utils.Dto.File;

namespace StudentTransfer.Api.Controllers;

[Authorize]
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
    public async Task<ActionResult<List<ApplicationDto>>> GetAll()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid)!;
        var isAdmin = User.IsInRole(RoleConstants.Admin);

        if (isAdmin)
            return await _service.GetAllAsync();
        
        return await _service.GetAllByUserAsync(Guid.Parse(userId));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApplicationDto>> GetById(int id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid)!;
        var isAdmin = User.IsInRole(RoleConstants.Admin);
        
        var applicationDto = await _service.GetByIdAsync(id);
        
        if (applicationDto == null)
            return NotFound();

        if (applicationDto.UserId != Guid.Parse(userId) && !isAdmin)
            return Forbid();
            
        return Ok(applicationDto);
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationDto>> AddApplication([FromForm]CreateApplicationRequest applicationRequest, List<IFormFile> formFiles)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid)!;

        var dto = await _service.CreateAsync(applicationRequest, Guid.Parse(userId));

        if (dto == null)
            return BadRequest();
        
        var fileRequests = formFiles
            .Select(formFile => new UploadFileRequest(formFile.FileName, dto.Id, formFile.OpenReadStream())).ToList();

        var fileDtos = await _fileService.UploadAsync(fileRequests, Guid.Parse(userId));
        dto.Files = fileDtos;

        return CreatedAtAction("AddApplication", dto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteApplication(int id)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid)!;
        var isAdmin = User.IsInRole(RoleConstants.Admin);
        
        var applicationDto = await _service.GetByIdAsync(id);
        if (applicationDto == null)
            return NotFound();
        
        if (applicationDto.UserId != Guid.Parse(userId) && !isAdmin)
            return Forbid();
        
        var success = await _service.TryDeleteAsync(id);
        
        var files = applicationDto.Files;

        if (success && files != null)
            foreach (var file in files)
                await _fileService.TryDeleteAsync(file.Id);
        
        if (success)
            return NoContent();
        return BadRequest();
    }
}