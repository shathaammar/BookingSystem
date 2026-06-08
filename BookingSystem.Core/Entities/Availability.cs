namespace BookingSystem.Core.Entities
{
    public class Availability : BaseEntity
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
