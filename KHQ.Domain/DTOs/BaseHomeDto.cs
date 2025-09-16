namespace KHQ.Domain.DTOs
{
    public class BaseHomeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SectionType { get; set; }
    }

}
