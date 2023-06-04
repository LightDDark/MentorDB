using Domain.Priv;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Out {
    public class AlgoMission {
        /*{"id": 1, "priority": "high", "length": 60, "deadline": datetime(2023, 4, 13, 9, 0, 0), "isRepeat": False,
         "optionalDays": ["Sunday"],
         "optionalHours": [10.50, 12.50],
         "rankListHistory": [
             {"rank": 6, "startTime": datetime(2023, 4, 3, 11, 30), "endTime": datetime(2023, 4, 3, 12, 30)},
             {"rank": 2, "startTime": datetime(2023, 4, 9, 9, 0), "endTime": datetime(2023, 4, 9, 10, 0)},
             { "rank": 6, "startTime": datetime(2023, 4, 8, 12, 0), "endTime": datetime(2023, 4, 8, 13, 0)}
         ],
         "type": "A",
         }*/
        public int Id { get; set; }
        public string Priority { get; set; }
        public int Length { get; set; }
        public DateTime DeadLine { get; set; }
        public bool IsRepeat { get; set; }
        public List<string> OptionalDays { get; set; } = new List<string>();
        public List<string> OptionalHours { get; set; } = new List<string>(2);
        public List<MissionRank> RankListHistory { get; set; } = new List<MissionRank>();
        public string? Type { get; set; }
    }
}
