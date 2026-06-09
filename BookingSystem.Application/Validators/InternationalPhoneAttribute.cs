using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BookingSystem.Application.Validators
{
    public class InternationalPhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Phone number is required");

            var phone = value?.ToString();

            if (string.IsNullOrWhiteSpace(phone))
            {
                return new ValidationResult("Phone number is required");
            }

            var regex = new Regex(@"^\+[1-9]\d{7,14}$");

            if (!regex.IsMatch(phone))
            {
                return new ValidationResult("Phone must be in international format like +9627XXXXXXX");
            }

            return ValidationResult.Success;
        }
    }
}