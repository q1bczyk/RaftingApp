using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.ReservationsDTO.Validator
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue < DateTime.Today)
                {
                    return new ValidationResult("Data wykonania nie może być wcześniejsza niż dzisiejsza.");
                }
            }
            return ValidationResult.Success;
        }
    }
}