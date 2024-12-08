using System;
using System.ComponentModel.DataAnnotations;
using Project.Core.DTO.SettingsDTO;

namespace Project.Core.Validation
{
    public class BookingValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not BaseSettingsDTO settings)
                return new ValidationResult("Invalid object type for validation.");

            if (settings.DayEarliestBookingTime < settings.DayLatestBookingTime)
                return new ValidationResult("Najpóźniejsza dzień otwarcia nie może być większy niz najwcześniejszy");

            return ValidationResult.Success;
        }
    }
}