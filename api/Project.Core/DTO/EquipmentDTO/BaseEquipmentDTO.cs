using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.EquipmentDTO
{
    public class BaseEquipmentDTO
    {
        [Required, MinLength(2)]
        public string TypeName { get; set; }
        [Required]
        public int MinParticipants { get; set; }
        [Required]
        public int MaxParticipants { get; set; }
        [ParticipantsValidation] 
        public object Participants => new { MinParticipants, MaxParticipants };
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Cena na osobę musi być liczbą nieujemną.")]
        public int PricePerPerson { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinParticipants > MaxParticipants)
            {
                yield return new ValidationResult(
                    "Minimalna liczba uczestników nie może być większa niż maksymalna liczba uczestników.",
                    new[] { nameof(MinParticipants), nameof(MaxParticipants) }
                );
            }
        }

    }
}