using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spa.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; } 

        public Department? Department { get; set; }
    }
}