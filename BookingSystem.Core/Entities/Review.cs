namespace BookingSystem.Core.Entities
{
    public class Review : BaseEntity
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public string CustomerId { get; set; } = string.Empty;
        public ApplicationUser Customer { get; set; } = null!;

        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
