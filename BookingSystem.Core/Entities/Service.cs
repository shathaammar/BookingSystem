namespace BookingSystem.Core.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }


        public int BusinessId { get; set; }
        public Business Business { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}