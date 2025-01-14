using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.ReservationsDTO;

namespace Project.Core.DTO.DashboardDTO
{
    public class DashboardDataDTO
    {
        public List<GetReservationDTO> Reservations { get; set; }
        public List<GetEquipmentTypeDTO> Equipment { get; set; }
    }
}