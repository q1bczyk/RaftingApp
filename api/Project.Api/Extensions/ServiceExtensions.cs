using AutoMapper;
using Project.Core.DTO.Auth;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IBusinessServices;
using Project.Core.Interfaces.IServices.IOtherServices;
using Project.Core.Mapper;
using Project.Core.Services.BusinessService;
using Project.Core.Services.OtherServices;
using Project.Infrastructure.Repositories;

namespace Project.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            //other services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IFileService, FileService>();

            //business services
            services.AddScoped<IEquipmentTypeService, EquipmentTypeService>();

            //repositories
            services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RegisterDTO, User>();
                cfg.CreateMap<AddEquipmentTypeDTO, EquipmentType>();
                cfg.CreateMap<EquipmentType, GetEquipmentTypeDTO>();
            });

            IMapper mapper = config.CreateMapper();

            //mapper to model
            services.AddSingleton<IBaseMapper<RegisterDTO, User>>(new BaseMapper<RegisterDTO, User>(mapper));
            services.AddSingleton<IBaseMapper<AddEquipmentTypeDTO, EquipmentType>>(new BaseMapper<AddEquipmentTypeDTO, EquipmentType>(mapper));

            //mapper to dto
            services.AddSingleton<IBaseMapper<EquipmentType, GetEquipmentTypeDTO>>(new BaseMapper<EquipmentType, GetEquipmentTypeDTO>(mapper));


            return services;
        }
    }
}