using Domain.Priv;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Out {
    public class UiMission {
        [Key] public int Id { get; set; }
        public string Title { get; set; } = new string("Default");
        public string? Description { get; set; } = new string("Default");
        public string? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Setteled { get; set; }
        public List<DayString> OptionalDays { get; set; } = new List<DayString>();
        public List<HourString> OptionalHours { get; set; } = new List<HourString>();
    }
}
