﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Role
    {
        public int Id { get; set; }
        public  string? Definition { get; set; }
        public List<User>? Users { get; set; }
      
    }
}
