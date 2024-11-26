using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ParticipantsValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var minParticipantsProperty = validationContext.ObjectType.GetProperty("MinParticipants");
        var maxParticipantsProperty = validationContext.ObjectType.GetProperty("MaxParticipants");

        if (minParticipantsProperty == null || maxParticipantsProperty == null) return new ValidationResult("Brak wymaganych właściwości MinParticipants lub MaxParticipants.");
        
        var minParticipants = (int)minParticipantsProperty.GetValue(validationContext.ObjectInstance);
        var maxParticipants = (int)maxParticipantsProperty.GetValue(validationContext.ObjectInstance);

        if (minParticipants <= 0) return new ValidationResult("Minimalna liczba uczestników musi być większa niż 0.");
        if (maxParticipants <= 0) return new ValidationResult("Maksymalna liczba uczestników musi być większa niż 0.");
        if (minParticipants > maxParticipants) return new ValidationResult("Minimalna liczba uczestników nie może być większa niż maksymalna liczba uczestników.");

        return ValidationResult.Success;
    }
}
