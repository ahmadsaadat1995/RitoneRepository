using OfficeOpenXml;
using Ritone.Application.Interfaces;
using Ritone.Infrastructure;

namespace Ritone.Application.Services
{
    public class ExcelProcessingService : IExcelProcessingService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExcelProcessingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ProcessExcelFileAsync(Stream fileStream)
        {
            using (var package = new ExcelPackage(fileStream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var record = new SampleEntity
                    {
                        FirstName = worksheet.Cells[row, 1].Text // Adjust based on your Excel structure
                    };
                    //_unitOfWork.Records.Add(record);
                }
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
