using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Domain.Entities
{
    public class Emails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SentEmail { get; set; }
        public string Subject { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
