using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Domain.ViewModel
{
    public class SliderVM
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public List<string> PathLink { get; set; }
    }
}
