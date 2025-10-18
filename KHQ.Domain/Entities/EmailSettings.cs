using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Domain.Entities
{
    public class EmailSettings
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Email { get; set; }
        public string Regards { get; set; }
        public string SupportTeam { get; set; }

    }
}
