namespace KHQ.Domain.DTOs
{
    public class StainDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public string CoverLink { get; set; }
        public Guid StainsId { get; set; }
    }
}
