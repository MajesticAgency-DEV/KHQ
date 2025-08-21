namespace KHQ.Domain.Entities
{
    public class BaseHome
    {
        public Guid Id { get; set; }
        public int SectionType { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
    }
}
