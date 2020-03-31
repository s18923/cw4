﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Models
{
    public class SemesterData
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        
        public int Semester { get; set; }
    }
}
