using Domain.Priv;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Out {
    public class UiMission {
        private const string DEFAULT = "Default";
        private const int DEFAULT_RANK = 4;
        [Key] public int Id { get; set; }
        public string Title { get; set; } = new string(DEFAULT);
        public string? Description { get; set; } = new string(DEFAULT);
        public string? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Settled { get; set; }
        public List<DayString> OptionalDays { get; set; } = new List<DayString>();
        public List<HourString> OptionalHours { get; set; } = new List<HourString>();
        public int Rank { get; set; } = DEFAULT_RANK;
    }
}
