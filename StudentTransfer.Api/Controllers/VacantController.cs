using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Bll.Services.Vacant;
using StudentTransfer.Utils;
using StudentTransfer.Utils.Dto.Vacant;

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
    public async Task<ActionResult<List<VacantDirectionDto>>> GetAllAsync()
    {
        var dtoList = await _vacantService.GetAllAsync();
        return Ok(dtoList);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VacantDirectionDto>> GetByIdAsync(int id)
    {
        var dto = await _vacantService.GetByIdAsync(id);

        if (dto == null)
            return NotFound();
        return Ok(dto);
    }
    
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost("update")]
    public async Task<IActionResult> UpdateDatabaseAsync()
    {
        await _vacantService.UpdateParseAsync();
        return NoContent();
    }
}