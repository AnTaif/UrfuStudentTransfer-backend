using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Bll.Services.Application;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Utils.Dto.Application;

namespace StudentTransfer.Api.Controllers;

[ApiController]
[Route("api/application")]
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
            var filePath = Path.Combine(fileRootPath, fileId.ToString());

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            var fileDto = new FileDto
            {
                Id = fileId,
                //OwnerId = Guid.NewGuid(), //TODO: UserId
                Name = formFile.FileName,
                Extension = Path.GetExtension(formFile.FileName),
                Path = filePath,
                UploadDate = DateTime.UtcNow //TODO: Change to local timestamp?
            };
            
            fileDtos.Add(fileDto);
        }
        
        var dto = await _service.CreateAsync(applicationRequest, fileDtos);

        return Ok(dto);
    }
}