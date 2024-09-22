using OfficeOpenXml;
using Ritone.Application;
using Ritone.EndPoint;

public class ExcelProcessor : IExcelProcessor
{
    private readonly ApplicationDbContext _context;

    public ExcelProcessor(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ProcessExcelFileAsync(IFormFile file)
    {
        if (file == null || file.Length <= 0)
        {
            throw new ArgumentException("فایل معتبر نیست.");
        }

        var records = new List<SampleEntity>();

        try
        {
            using var stream = file.OpenReadStream();
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                throw new InvalidOperationException("Sheet خالی است یا فایل معتبر نیست.");
            }

            int rowCount = worksheet.Dimension?.Rows ?? 0;

            // شروع از ردیف 2 به دلیل هدر
            for (int row = 2; row <= rowCount; row++)
            {
                var record = new SampleEntity
                {
                    FirstName = worksheet.Cells[row, 1].GetValue<string>(),
                    LastName = worksheet.Cells[row, 2].GetValue<string>(),
                    Email = worksheet.Cells[row, 3].GetValue<string>(),
                    Phone = worksheet.Cells[row, 4].GetValue<string>(),
                    Quantity = worksheet.Cells[row, 5].GetValue<int>(),
                    Price = worksheet.Cells[row, 6].GetValue<decimal>(),
                    Address = worksheet.Cells[row, 7].GetValue<string>()
                };

                records.Add(record);
            }
            //save to databse
            await SaveRecordsToDatabaseAsync(records);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("خطا در پردازش فایل Excel: " + ex.Message);
        }
    }

    private async Task SaveRecordsToDatabaseAsync(List<SampleEntity> records)
    {
        await _context.RitoneEntity.AddRangeAsync(records);
        await _context.SaveChangesAsync();
    }
}
