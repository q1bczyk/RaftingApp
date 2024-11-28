using Project.Core.DTO.EquipmentDTO;

namespace Project.Core.DTO.ReservationEquipmentDTO
{
    public class GetReservationEquipmentDTO : AddReservationEquipmentDTO
    {
        public string Id { get; set; }
        public GetEquipmentTypeDTO EquipmentType { get; set; }
    }
}