namespace KHQ.Domain.ViewModel
{
    public class SubProductVM
    {
        public Guid Id { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string ImageUrl { get; set; }
        public Guid ProductId { get; set; }
        public ProductVM Product { get; set; }
    }
}
