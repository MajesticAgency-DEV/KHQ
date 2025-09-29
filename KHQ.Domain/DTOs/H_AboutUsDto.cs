using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Domain.DTOs
{
    public class H_AboutUsDto
    {
        public Guid Id { get; set; }
        public string Point { get; set; }
        public string Icon { get; set; }
        public string ImageLink { get; set; }

    }
    public class H_AboutUsDtoNew
    {
        public List<H_AboutUsDto> H_Aboutus { get; set; }
        public string Main_Title { get; set; }
        public string Main_Description { get; set; }
    }
}
