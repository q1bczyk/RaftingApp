using AutoMapper;
using Project.Core.DTO.Auth;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IServices.IOtherServices;
using Project.Core.Mapper;
using Project.Core.Services.OtherServices;

namespace Project.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            //other services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            //repositories

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RegisterDTO, User>();
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton<IBaseMapper<RegisterDTO, User>>(new BaseMapper<RegisterDTO, User>(mapper));

            return services;
        }
    }
}