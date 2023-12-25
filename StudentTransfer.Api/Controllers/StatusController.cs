using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using StudentTransfer.Bll.Services.Application;
using StudentTransfer.Bll.Services.StatusServices;
using StudentTransfer.Dal.Entities.Application;
using StudentTransfer.Utils;
using StudentTransfer.Utils.Dto.StatusDtos;

namespace StudentTransfer.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/applications/{applicationId:int}/status")]
public class StatusController : ControllerBase
{
    private readonly IApplicationService _applicationService;
    private readonly IStatusService _statusService;

    public StatusController(IApplicationService applicationService, IStatusService statusService)
    {
        _applicationService = applicationService;
        _statusService = statusService;
    }

    [HttpGet]
    public async Task<ActionResult<ApplicationStatus>> GetCurrentStatus(int applicationId)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid)!;
        var isAdmin = User.IsInRole(RoleConstants.Admin);

        var application = await _applicationService.GetByIdAsync(applicationId);
        if (application == null)
            return NotFound();

        if (application.UserId != Guid.Parse(userId) && !isAdmin)
            return Forbid();
        
        var currentStatus = await _statusService.GetApplicationStatusAsync(applicationId);

        if (currentStatus == null)
            return BadRequest();
        return Ok(currentStatus);
    }
    
    [HttpGet("history")]
    public async Task<ActionResult<List<ApplicationStatus>>> GetStatusHistory(int applicationId)
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sid)!;
        var isAdmin = User.IsInRole(RoleConstants.Admin);

        var application = await _applicationService.GetByIdAsync(applicationId);
        if (application == null)
            return NotFound();

        if (application.UserId != Guid.Parse(userId) && !isAdmin)
            return Forbid();
        
        var statusHistory = await _statusService.GetStatusHistoryAsync(applicationId);

        if (statusHistory == null)
            return BadRequest();
        return Ok(statusHistory);
    }
    
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost]
    public async Task<IActionResult> UpdateApplicationStatus(UpdateStatusRequest request, int applicationId)
    {
        var result = await _statusService.TryUpdateStatusAsync(request, applicationId);

        if (result)
            return NoContent();
        return BadRequest();
    }
}
