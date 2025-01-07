using Project.Core.DTO.EquipmentDTO;
using Project.Core.DTO.PaymentDTO;

namespace Project.Core.DTO.ReservationEquipmentDTO
{
    public class GetReservationEquipmentDTO 
    {
        public GetEquipmentTypeDTO EquipmentType { get; set; }
        public int Quantity{ get; set; }
        public int Participants { get; set; }
    }
}