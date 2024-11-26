using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.EquipmentDTO
{
    [ParticipantsValidation]
    public class BaseEquipmentDTO
    {
        [Required, MinLength(2)]
        public string TypeName { get; set; }
        [Required]
        public int MinParticipants { get; set; }
        [Required]
        public int MaxParticipants { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Cena na osobę musi być liczbą nieujemną.")]
        public int PricePerPerson { get; set; }

    }
}