using Project.Core.DTO.ReservationsDTO;

namespace Project.Core.Interfaces.IServices.IBusinessServices
{
    public interface IReservationService : IBaseCrudService<GetReservationDTO, AddReservationDTO, AddReservationDTO>
    {}
}