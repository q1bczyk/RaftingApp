using AutoMapper;
using Project.Core.DTO.Auth;
using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.ReservationEquipmentDTO;
using Project.Core.DTO.ReservationsDTO;
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
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IReservationEquipmentService, ReservationEquipmentService>();

            //repositories
            services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationEquipmentRepository, ReservationEquipmentRepository>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RegisterDTO, User>();
                cfg.CreateMap<AddEquipmentTypeDTO, EquipmentType>();
                cfg.CreateMap<EquipmentType, GetEquipmentTypeDTO>();
                cfg.CreateMap<AddReservationDTO, Reservation>();
                cfg.CreateMap<Reservation, GetReservationDTO>()
                     .ForMember(dest => dest.ReservationEquipment, opt => opt.MapFrom(src => src.ReservationEquipment));
                cfg.CreateMap<AddReservationEquipmentDTO, ReservationEquipment>();
                cfg.CreateMap<ReservationEquipment, GetReservationEquipmentDTO>()
                    .ForMember(dest => dest.EquipmentType, opt => opt.MapFrom(src => src.EquipmentType));
            });

            IMapper mapper = config.CreateMapper();

            //mapper to model
            services.AddSingleton<IBaseMapper<RegisterDTO, User>>(new BaseMapper<RegisterDTO, User>(mapper));
            services.AddSingleton<IBaseMapper<AddEquipmentTypeDTO, EquipmentType>>(new BaseMapper<AddEquipmentTypeDTO, EquipmentType>(mapper));
            services.AddSingleton<IBaseMapper<AddReservationDTO, Reservation>>(new BaseMapper<AddReservationDTO, Reservation>(mapper));
            services.AddSingleton<IBaseMapper<AddReservationEquipmentDTO, ReservationEquipment>>(new BaseMapper<AddReservationEquipmentDTO, ReservationEquipment>(mapper));

            //mapper to dto
            services.AddSingleton<IBaseMapper<EquipmentType, GetEquipmentTypeDTO>>(new BaseMapper<EquipmentType, GetEquipmentTypeDTO>(mapper));
            services.AddSingleton<IBaseMapper<Reservation, GetReservationDTO>>(new BaseMapper<Reservation, GetReservationDTO>(mapper));
            services.AddSingleton<IBaseMapper<ReservationEquipment, GetReservationEquipmentDTO>>(new BaseMapper<ReservationEquipment, GetReservationEquipmentDTO>(mapper));


            return services;
        }
    }
}