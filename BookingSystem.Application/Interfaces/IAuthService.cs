using BookingSystem.Application.Common;
using BookingSystem.Application.DTOs.Auth;

namespace BookingSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthResponseDto>> RegisterCustomerAsync(RegisterCustomerDto dto);
        Task<Result<AuthResponseDto>> RegisterBusinessOwnerAsync(RegisterBusinessOwnerDto dto);
        Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto);
    }
}
