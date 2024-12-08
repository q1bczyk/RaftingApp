using System;
using System.ComponentModel.DataAnnotations;
using Project.Core.DTO.SettingsDTO;

namespace Project.Core.Validation
{
    public class OpeningHoursValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not BaseSettingsDTO settings)
                return new ValidationResult("Invalid object type for validation.");

            if (settings.OpeningTime.TimeOfDay >= settings.CloseTime.TimeOfDay)
                return new ValidationResult("OpeningTime must be earlier than CloseTime.");

            return ValidationResult.Success;
        }
    }
}
