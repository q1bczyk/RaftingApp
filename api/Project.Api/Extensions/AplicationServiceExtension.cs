
using Microsoft.EntityFrameworkCore;
using Project.Api.Helpers;
using Project.Core.Config;
using Project.Core.Helpers;
using Project.Infrastructure.Data;

namespace Project.Api.Extensions
{
    public static class AplicationServiceExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<DataContext>(opt => {
                opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<EmailConfig>(config.GetSection("EmailConfig"));
            services.Configure<AzureBlobConfig>(config.GetSection("AzureBlobConfig"));
            services.Configure<StripeConfig>(config.GetSection("StripeConfig"));

            return services;
        }
    }
}