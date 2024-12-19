using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.ReservationEquipmentDTO
{
    public class AddReservationEquipmentDTO
    {
        [Required]
        public string EquipmentTypeId { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Ilość danego musi być większa bądź równa 1")]
        public int Quantity{ get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Ilość danego musi być większa bądź równa 1")]
        public int Participants{ get; set; }
    }
}