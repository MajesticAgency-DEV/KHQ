namespace KHQ.Domain.DTOs
{
    public class StainsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
    }
    public class StainsDtoNew
    {
        public List<StainsDto> Stains { get; set; }
        public string Main_Title { get; set; }
        public string Main_Description { get; set; }
    }

}
