namespace KHQ.Domain.Entities
{
    public class Slider
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string ButtonTextEn { get; set; }
        public string ButtonTextAr { get; set; }
    }

    public class SliderImages : ValueObject
    {
        public Guid Id { get; set; }
        public Guid SliderId { get; set; }
        public string ImagePath { get; set; }
    }
}
