using Project.Core.DTO.ReservationsDTO;

namespace Project.Core.Interfaces.IServices.IOtherServices
{
    public interface IReservationValidationService
    {
        Task ValidReservation(AddReservationDTO reservationDetails);
    }
}