using AutoMapper;

namespace Project.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new MapperConfiguration(cfg => {

            });

            IMapper mapper = config.CreateMapper();

            return services;
        }
    }
}