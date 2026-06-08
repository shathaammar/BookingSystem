using BookingSystem.Core.Enums;

namespace BookingSystem.Core.Entities
{
    public class Payment : BaseEntity
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;

        public decimal Amount { get; set; }

        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }

        public DateTime PaidAt { get; set; }
    }
}
