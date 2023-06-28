using Domain.Priv;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Out {
    public class AlgoMission {
        public int Id { get; set; }
        public string Priority { get; set; }
        public int Length { get; set; }
        public DateTime DeadLine { get; set; }
        public bool IsRepeat { get; set; }
        public List<string> OptionalDays { get; set; } = new List<string>();
        public List<string> OptionalHours { get; set; } = new List<string>(2);
        public List<MissionRank> RankListHistory { get; set; } = new List<MissionRank>();
        public string? Type { get; set; }
        public string? Description { get; set; }
    }
}
