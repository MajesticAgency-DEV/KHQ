using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Domain.DTOs
{
    public class ImageDto
    {
        public Guid Id { get; set; }
        public Guid F_Key { get; set; }
        public string PathLink { get; set; }
        public ImageType ImageType { get; set; }
        public string? ImageName { get; set; }
        public int Sort { get; set; }
    }
}
