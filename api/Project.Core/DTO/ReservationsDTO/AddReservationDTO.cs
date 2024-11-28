using Project.Core.DTO.ReservationEquipmentDTO;

namespace Project.Core.DTO.ReservationsDTO
{
    public class AddReservationDTO : BaseReservationDTO
    {
        AddReservationEquipmentDTO ReservationEquipment { get; set; }
    }
}