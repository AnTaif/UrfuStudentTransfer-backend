namespace StudentTransfer.Api.Controllers;

// [ApiController]
// [Route("uploads")]
// public class UploadsController : ControllerBase
// {
//     private readonly IHostEnvironment _hostEnvironment;
//     private readonly IFileService _fileService;
//
//     public UploadsController(IHostEnvironment hostEnvironment, IFileService fileService)
//     {
//         _hostEnvironment = hostEnvironment;
//         _fileService = fileService;
//     }
//     
//     [HttpGet("{id}")]
//     public async Task<IActionResult> GetById(Guid id)
//     {
//         var fileResponse = await _fileService.GetPhysicalFileAsync(id);
//
//         if (fileResponse == null)
//             return NotFound();
//
//         return File(fileResponse.Stream, "application/octet-stream", fileResponse.FileName);
//     }
// }