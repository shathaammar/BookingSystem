using BookingSystem.Application.Common;
using BookingSystem.Application.DTOs.Business;

namespace BookingSystem.Application.Interfaces
{
    public interface IBusinessService
    {
        Task<Result<int>> CreateAsync(CreateBusinessDto dto, string ownerId);
    }
}
