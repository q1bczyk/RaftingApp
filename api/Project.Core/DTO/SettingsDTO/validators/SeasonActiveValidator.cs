using System;
using System.ComponentModel.DataAnnotations;
using Project.Core.DTO.SettingsDTO;

namespace Project.Core.Validation
{
    public class SeasonActiveValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not BaseSettingsDTO settings)
                return new ValidationResult("Invalid object type for validation.");

            if (settings.SeasonStartDate >= settings.SeasonEndDate)
                return new ValidationResult("SeasonStartDate must be earlier than SeasonEndDate.");

            return ValidationResult.Success;
        }
    }
}
