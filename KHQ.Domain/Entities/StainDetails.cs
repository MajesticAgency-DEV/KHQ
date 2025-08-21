namespace KHQ.Domain.Entities
{
    public class StainDetails
    {
        public Guid Id { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public Guid StainsId { get; set; }
    }
}
