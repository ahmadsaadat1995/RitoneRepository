using Ritone.Application.Interfaces;
using Ritone.Application.Services;
using Ritone.Infrastructure;

namespace ExcelUploadApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExcelProcessingService, ExcelProcessingService>();
            return services;
        }
    }
}
