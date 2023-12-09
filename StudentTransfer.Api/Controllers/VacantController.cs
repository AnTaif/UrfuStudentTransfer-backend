using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Bll.Services.Vacant;

namespace StudentTransfer.Api.Controllers;

[ApiController]
[Route("api/vacant")]
public class VacantController : ControllerBase
{
    private readonly IVacantService _vacantService;

    public VacantController(IVacantService vacantService)
    {
        _vacantService = vacantService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var dtoList = await _vacantService.GetAllAsync();
        return Ok(dtoList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var dto = await _vacantService.GetByIdAsync(id);

        return Ok(dto);
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateDatabaseAsync()
    {
        await _vacantService.UpdateParseAsync();
    
        return Ok();
    }
}