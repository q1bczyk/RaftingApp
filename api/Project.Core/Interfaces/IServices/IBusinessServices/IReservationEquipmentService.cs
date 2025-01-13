using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.ReservationsDTO;
using Project.Core.DTO.ReservationEquipmentDTO;
using Project.Core.Entities;

namespace Project.Core.Interfaces.IServices.IBusinessServices
{
    public interface IReservationEquipmentService 
    {
        public Task<List<GetEquipmentTypeDTO>> FetchAvaiableEquipment(ReservationDetailsDTO reservationDetailsDTO);
    }
}