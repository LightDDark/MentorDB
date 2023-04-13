using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    /*"startHour": "9:00:00",
        "endHour": "18:00:00",
        "minGap": 15,
        "maxHoursPerDay": 5,
        "maxHoursPerTypePerDay": {"A": 3, "B": 2},
        "minTimeFrame": 15*/
    public class ScheduleSetting {
        [Key] int id;
        DateTime StartHour { get; set; } = new DateTime(0, 0, 0, 9, 0, 0);
        DateTime EndHour { get; set; } = new DateTime(0, 0, 0, 18, 0, 0);
        int MinGap { get; set; } = 15;
        int MaxHoursPerDay { get; set; } = 5;
        int MinTimeFrame { get; set; } = 15;
    }
}
