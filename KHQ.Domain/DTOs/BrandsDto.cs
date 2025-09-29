namespace KHQ.Domain.DTOs
{
    public class BrandsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FaceLink { get; set; }
        public string InstaLink { get; set; }
        public string ImageLink { get; set; }

    }
    public class BrandDtoNew
    {
        public List<BrandsDto> BrandsDtos { get; set; }
        public string Title { get; set; }
        public string Main_Description { get; set; }
        public string CoverPhoto { get; set; }

    }
}
