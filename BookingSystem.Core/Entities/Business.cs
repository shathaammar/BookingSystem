namespace BookingSystem.Core.Entities
{
    public class Business : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;

        public string BusinessOwnerId { get; set; } = string.Empty;
        public ApplicationUser BusinessOwner { get; set; } = null!;

        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
