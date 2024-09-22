using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Ritone.Application.Interfaces;

namespace ExcelUploadApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelProcessingService _excelProcessingService;

        public ExcelController(IExcelProcessingService excelProcessingService)
        {
            _excelProcessingService = excelProcessingService;
        }

        [HttpPost("upload")]
        public IActionResult Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Enqueue the job for processing
            BackgroundJob.Enqueue(() => _excelProcessingService.ProcessExcelFileAsync(file.OpenReadStream()));

            return Accepted(); // Responds immediately
        }
    }
}
