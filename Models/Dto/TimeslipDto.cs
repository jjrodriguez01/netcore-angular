using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spa.Models.Dto
{
    public class TimeslipDto
    {
        public int TimeslipId { get; set; }
        public string? Comment { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class CreateTimeslipDto : TimeslipDto
    {
        public string UserId { get; set; } = string.Empty;
        public int TaskId {get; set;}
    }

    public class TimeslipViewDto : TimeslipDto 
    {
        public string? UserName {get; set;}
        public string? ShortDate {get; set;}
        public string? Hour {get; set;}
        public string? DayOfWeek {get; set;}
        public string? TaskName {get; set;}
        public string? DepartmentName {get; set;}
    }
}