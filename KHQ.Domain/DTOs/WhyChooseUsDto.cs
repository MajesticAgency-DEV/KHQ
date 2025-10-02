using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Domain.DTOs
{
    public class WhyChooseUsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
    public class WhyChooseUsDtoNew
    {
        public List<WhyChooseUsDto> WhyChooseUs { get; set; }
        public string Main_Title { get; set; }
        public string Main_Description { get; set; }
    }
}
