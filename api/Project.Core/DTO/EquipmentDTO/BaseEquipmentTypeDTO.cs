using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.EquipmentDTO
{
    [ParticipantsValidation]
    public class BaseEquipmentTypeDTO
    {
        private string _typeName;
        [Required, MinLength(2)]
        public string TypeName { get => _typeName; set => _typeName = value?.ToLower(); }
        [Required]
        public int MinParticipants { get; set; }
        [Required]
        public int MaxParticipants { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Cena na osobę musi być liczbą nieujemną.")]
        public int PricePerPerson { get; set; }

    }
}