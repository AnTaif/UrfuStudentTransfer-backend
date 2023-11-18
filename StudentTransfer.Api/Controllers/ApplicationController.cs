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

    public ApplicationController(IApplicationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<ApplicationRequest>> GetAll()
    {
        return await _service.GetAllAsync();
    }

    [HttpPost]
    public async Task AddApplication([FromBody]ApplicationDto application, List<IFormFile> files)
    {
        var a = application;

        //await _service.AddAsync(application);
    }
    
    
}