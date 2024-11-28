using Project.Core.DTO.ReservationEquipmentDTO;

namespace Project.Core.Interfaces.IServices.IBusinessServices
{
    public interface IReservationEquipmentService 
    {
        public Task AddMany(List<AddReservationEquipmentDTO> addReservationEquipmentDTOs, string reservationId);
        public Task DeleteAll(string reservationId);
        public Task DeleteSingle(string reservationId, string equipmentId);
    }
}