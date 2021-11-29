using System.ComponentModel.DataAnnotations;

namespace Spendee.Shared.DataAnnotations;

public class NotZero : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var input = Convert.ToInt32(value);
        var errorMessage = "The value must not be 0";

        if (input != 0) return ValidationResult.Success;
        return new ValidationResult(errorMessage, new List<string>() { validationContext.MemberName });
    }
}
