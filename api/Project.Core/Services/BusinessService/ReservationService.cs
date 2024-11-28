using Project.Core.DTO.ReservationsDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IBusinessServices;

namespace Project.Core.Services.BusinessService
{
    public class ReservationService : BaseCrudService<
    GetReservationDTO,
    AddReservationDTO,
    AddReservationDTO,
    Reservation,
    IReservationRepository
    >, IReservationService
    {
        private readonly IReservationEquipmentService _reservationEquipmentService;
        public ReservationService(IReservationRepository repository, IBaseMapper<AddReservationDTO, Reservation> toModelMapper, IBaseMapper<Reservation, GetReservationDTO> toDTOMapper, IReservationEquipmentService reservationEquipmentService) : base(repository, toModelMapper, toDTOMapper)
        {
            _reservationEquipmentService = reservationEquipmentService;
        }

        public async override Task<GetReservationDTO> Create(AddReservationDTO createDTO)
        {

            var reservationModel = _toModelMapper.MapToModel(createDTO);
            var addedModel = await _repository.Create(reservationModel);
            await _reservationEquipmentService.AddMany(createDTO.ReservationEquipment, addedModel.Id);
            
            return _toDTOMapper.MapToModel(reservationModel);
        }
    }
}