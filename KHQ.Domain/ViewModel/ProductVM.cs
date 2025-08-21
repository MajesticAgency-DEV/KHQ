namespace KHQ.Domain.ViewModel
{
    public class ProductVM
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public decimal Price { get; set; }
        public ICollection<SubProductVM> SubProducts { get; set; }
    }
}
