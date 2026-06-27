using BookingSystem.Core.Entities;

namespace BookingSystem.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
