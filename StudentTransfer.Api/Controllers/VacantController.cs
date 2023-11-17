using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Api.Dto;
using StudentTransfer.Dal.Entities.Vacant;
using StudentTransfer.Bll.Services.Vacant;
using StudentTransfer.Dal.Entities.Enums;

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
        var vacantDirections = dataList.ToList();
        var dtoList = vacantDirections.Select(e => new EducationDirectionDto
        {
            Id = e.Id,
            Code = e.Code,
            Name = e.Name,
            Level = EducationLevelConverter.ConvertToString(e.Level),
            Course = e.Course,
            Form = EducationFormConverter.ConvertToString(e.Form),
            FederalBudgets = e.FederalBudgets,
            SubjectsBudgets = e.SubjectsBudgets,
            LocalBudgets = e.LocalBudgets,
            Contracts = e.Contracts
        });
        return Ok(dtoList);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateDatabaseAsync()
    {
        await _vacantService.UpdateParseAsync();
    
        return Ok();
    }
}