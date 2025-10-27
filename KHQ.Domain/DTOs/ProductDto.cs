using KHQ.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Domain.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Capacity { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public ICollection<SubProductDto> SubProducts { get; set; }
    }

    public class ProductDtoNew
    {
        public string CoverPhoto { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public List<ProductDto> Products { get; set; }

    }
}
