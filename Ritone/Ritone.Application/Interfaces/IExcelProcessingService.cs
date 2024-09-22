using Microsoft.AspNetCore.Http;
namespace Ritone.Application.Interfaces
{
    public interface IExcelProcessingService
    {
        Task ProcessExcelFileAsync(Stream fileStream);
    }
}
