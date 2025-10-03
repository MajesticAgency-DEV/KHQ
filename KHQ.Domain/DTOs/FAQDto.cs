using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Domain.DTOs
{
    public class FAQDto
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
    public class FAQDtoNew
    {
        public string CoverPhoto { get; set; }
        public List<FAQDto> FAQs { get; set; }
        public BrouchuresDto Brouchures { get; set; }
    }
}
