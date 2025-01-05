using System.ComponentModel.DataAnnotations;
using Project.Core.DTO.PaymentDTO;
using Project.Core.DTO.ReservationEquipmentDTO;

namespace Project.Core.DTO.ReservationsDTO
{
    public class AddReservationDTO : BaseReservationDTO
    {
        [Required]
        public List<AddReservationEquipmentDTO> ReservationEquipment { get; set; }
        [Required]
        public PaymentConfirmationDTO Payment { get; set; }
    }
}