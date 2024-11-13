using Project.Core.Interfaces.IServices.IOtherServices;
using Project.Core.Services.OtherServices;

namespace Project.Api.Extensions
{
    public static class AplicationServiceExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}