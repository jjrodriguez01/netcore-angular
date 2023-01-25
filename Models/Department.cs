using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spa.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string? Name { get; set; }

        public List<Task>? Tasks { get;set; }
    }
}