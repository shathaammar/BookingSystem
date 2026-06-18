using BookingSystem.Core.Enums;

namespace BookingSystem.Core.Entities
{
    public class Booking : BaseEntity
    {
        public DateTime BookingDate { get; set; }


        public string CustomerId { get; set; } = string.Empty;
        public ApplicationUser Customer { get; set; } = null!;
        
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
        
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
    }
}