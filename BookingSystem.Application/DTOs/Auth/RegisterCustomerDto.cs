using BookingSystem.Application.Validators;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Application.DTOs.Auth
{
    public class RegisterCustomerDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [InternationalPhone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}