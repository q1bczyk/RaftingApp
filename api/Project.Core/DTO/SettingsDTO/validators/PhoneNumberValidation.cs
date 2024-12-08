using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int phoneNumber)
        {
            string phonePattern = @"^\d{9}$";
            if (!Regex.IsMatch(phoneNumber.ToString(), phonePattern))
                return new ValidationResult("Numer telefonu musi zawierać dokładnie 9 cyfr.");
        }
        return ValidationResult.Success;
    }
}