using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spa.Models
{
    public class Timeslip
    {
        public int TimeslipId { get; set; }
        public string? Comment { get; set; }
        public DateTime DateTime { get; set; }
        public int TaskId { get; set; }
        public string? ApplicationUserId { get; set; }

        public Task? Task { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}