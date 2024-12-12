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

            var differenceInDays = (settings.SeasonEndDate - settings.SeasonStartDate).TotalDays;
            if (differenceInDays < 1)
                return new ValidationResult("The difference between SeasonStartDate and SeasonEndDate must be at least one full day.");

            return ValidationResult.Success;
        }
    }
}
