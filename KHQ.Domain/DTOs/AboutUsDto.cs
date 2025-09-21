namespace KHQ.Domain.DTOs
{
    public class AboutUsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<H_AboutUsDto> H_AboutUsDto { get; set; }
        public string? CoverPhoto { get; set; }
        public string? AboutUsImage { get; set; }
    }
}
