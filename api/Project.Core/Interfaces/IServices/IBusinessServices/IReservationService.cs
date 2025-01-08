using Project.Core.DTO.ReservationsDTO;

namespace Project.Core.Interfaces.IServices.IBusinessServices
{
    public interface IReservationService : IBaseCrudService<GetReservationDTO, AddReservationDTO, AddReservationDTO>
    {
        public Task<List<GetReservationDTO>> GetFilteredData(
            DateTime? startDate = null,
            DateTime? endDate = null,
            DateTime? specificDate = null,
            string lastNamePartial = null,
            string reservationId = null 
        );
    }
}