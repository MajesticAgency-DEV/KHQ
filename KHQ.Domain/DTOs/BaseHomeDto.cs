﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHQ.Domain.DTOs
{
    public class BaseHomeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Descreption { get; set; }
    }

}
