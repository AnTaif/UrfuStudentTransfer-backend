using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTransfer.Utils;
using StudentTransfer.Utils.Dto.StatusDtos;

namespace StudentTransfer.Api.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/status")]
public class StatusController
{
    private readonly int _applicationId;
    
    public StatusController(int applicationId)
    {
        _applicationId = applicationId;
    }
    
    //TODO: [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost]
    public Task<ActionResult<UpdateStatusResponse>> UpdateApplicationStatus()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("history")]
    public Task<IActionResult> GetStatusHistory()
    {
        throw new NotImplementedException();
    }

    //TODO: [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost("next")]
    public Task<ActionResult<NextStatusResponse>> NextStatus()
    {
        throw new NotImplementedException();
    } 
}
