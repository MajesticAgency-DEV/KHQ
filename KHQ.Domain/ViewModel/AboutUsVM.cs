namespace KHQ.Domain.ViewModel
{
    public class AboutUsVM
    {
        public Guid Id { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string? ImageLink { get; set; }
    }
}
