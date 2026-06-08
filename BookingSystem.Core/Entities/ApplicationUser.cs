using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

    }
}