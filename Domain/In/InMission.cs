using Domain.Priv;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Mission;

namespace Domain.In {
    public class InMission {
        public int Id { get; set; }
        public string Title { get; set; } = new string("Default");
        public string? Description { get; set; } = new string("Default");
        public string? Type { get; set; }
        public int Length { get; set; } = 2;
        public List<DayString> OptionalDays { get; set; } = new List<DayString>();
        public List<HourString> OptionalHours { get; set; } = new List<HourString>() ;
        public DateTime DeadLine { get; set; }
        public Prior Priority { get; set; }
        public bool Settled { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
