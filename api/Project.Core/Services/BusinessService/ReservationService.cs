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
        public ReservationService(IReservationRepository repository, IBaseMapper<AddReservationDTO, Reservation> toModelMapper, IBaseMapper<Reservation, GetReservationDTO> toDTOMapper) : base(repository, toModelMapper, toDTOMapper)
        {}
    }
}