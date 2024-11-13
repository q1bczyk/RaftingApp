using AutoMapper;
using Project.Core.Interfaces.IServices.IOtherServices;
using Project.Core.Services.OtherServices;

namespace Project.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();

            var config = new MapperConfiguration(cfg => {

            });

            IMapper mapper = config.CreateMapper();

            return services;
        }
    }
}