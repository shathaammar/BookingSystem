namespace BookingSystem.Application.DTOs.Business
{
    public class BusinessDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
    }
}
