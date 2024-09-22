using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Ritone.Application;

[ApiController]
[Route("api/[controller]")]
public class FileUploadController : ControllerBase
{
    private readonly IBackgroundJobClient _backgroundJobClient;

    public FileUploadController(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }

    [HttpPost("upload")]
    public IActionResult UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Please upload a valid Excel file.");
        }
        var jobId = _backgroundJobClient.Enqueue<IExcelProcessor>(it => it.ProcessExcelFileAsync(file));
        return Accepted(new { JobId = jobId });
    }
}
