namespace BookingSystem.Application.DTOs.Business
{
    public class CreateBusinessDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
