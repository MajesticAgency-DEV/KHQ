namespace KHQ.Domain.Entities
{
    public class SubProduct
    {
        public Guid Id { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public int SortOrder { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
