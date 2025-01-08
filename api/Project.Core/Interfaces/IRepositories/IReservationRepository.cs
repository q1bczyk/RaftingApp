using Project.Core.Entities;

namespace Project.Core.Interfaces.IRepositories
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        public Task<List<Reservation>> GetFilteredData(
            DateTime? startDate = null,
            DateTime? endDate = null,
            DateTime? specificDate = null,
            string lastNamePartial = null,
            string reservationId = null
        );
        
    }
}