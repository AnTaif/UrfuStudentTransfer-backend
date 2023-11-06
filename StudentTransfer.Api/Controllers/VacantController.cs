using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Dal.Entities.Vacant;
using StudentTransfer.Logic.Services;
using StudentTransfer.VacantParser;

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
        var dataList = await _vacantService.GetAllAsync();
        return Ok(dataList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var data = await _vacantService.GetByIdAsync(id);
        if (data != null)
        {
            return Ok(data);
        }

        return BadRequest();
    }

    [HttpPost("list")]
    public async Task<IActionResult> AddListAsync(List<EducationDirection> dataList)
    {
        await _vacantService.AddEnumerableAsync(dataList);
        return Ok();
    }

    [HttpGet("update")]
    public async Task<IActionResult> UpdateDatabaseAsync()
    {
        await _vacantService.DeleteAllDataAsync();
        var parsedData = await VacantListParser.ParseVacantItemsAsync();
        await _vacantService.AddEnumerableAsync(parsedData);

        return Ok();
    }

    [HttpDelete("all")]
    public async Task<IActionResult> DeleteAllDataAsync()
    {
        await _vacantService.DeleteAllDataAsync();
        return Ok();
    }
}