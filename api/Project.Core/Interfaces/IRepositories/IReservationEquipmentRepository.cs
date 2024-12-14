using Project.Core.DTO.ReservationsDTO;
using Project.Core.Entities;

namespace Project.Core.Interfaces.IRepositories
{
    public interface IReservationEquipmentRepository : IBaseRepository<ReservationEquipment>
    {
        Task<List<EquipmentType>> GetAvaiableEquipmentAsync(ReservationDetailsDTO reservationDetailsDTO);
    }
}