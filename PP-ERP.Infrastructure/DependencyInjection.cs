using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PP_ERP.Application.Interfaces;
using PP_ERP.Application.UnitOfWork;
using PP_ERP.Infrastructure.Options;
using PP_ERP.Infrastructure.Persistence;
using PP_ERP.Infrastructure.Services;

namespace PP_ERP.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.Configure<AzureStorageOptions>(
                configuration.GetSection(AzureStorageOptions.SectionName));
            services.AddScoped<IBlobStorageService, BlobStorageService>();
            services.AddScoped<IImageProcessingService, ImageProcessingService>();

            return services;
        }
    }
}
