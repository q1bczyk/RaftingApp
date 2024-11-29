using Project.Core.DTO.ReservationEquipmentDTO;

namespace Project.Core.DTO.ReservationsDTO
{
    public class GetReservationDTO : BaseReservationDTO
    {
        public string Id { get; set; }
        public DateTime ReservationDate { get; set; } 
        public List<GetReservationEquipmentDTO> ReservationEquipment { get; set; }
    }
}