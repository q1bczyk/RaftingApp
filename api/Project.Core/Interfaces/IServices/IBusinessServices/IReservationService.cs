using Project.Core.DTO.ReservationsDTO;
using Project.Core.Entities;

namespace Project.Core.Interfaces.IServices.IBusinessServices
{
    public interface IReservationService : IBaseCrudService<GetReservationDTO, AddReservationDTO, AddReservationDTO>
    {
     
    }
}