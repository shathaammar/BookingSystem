using BookingSystem.Application.Common;
using BookingSystem.Application.DTOs.Business;
using BookingSystem.Application.Interfaces;
using BookingSystem.Core.Entities;
using BookingSystem.Infrastructure.Data;

namespace BookingSystem.Infrastructure.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly AppDbContext _context;

        public BusinessService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> CreateAsync(CreateBusinessDto dto, string ownerId)
        {
            try
            {
                var business = new Business
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Address = dto.Address,
                    BusinessOwnerId = ownerId,
                    IsApproved = false
                };

                _context.Businesses.Add(business);
                await _context.SaveChangesAsync();

                return Result<int>.Success(business.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure($"An error occurred while creating the business: {ex.Message}",
                    "حدث خطأ أثناء إنشاء النشاط التجاري");
            }
        }
    }
}
