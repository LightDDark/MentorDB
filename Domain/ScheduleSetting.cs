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
        [Key] public int Id { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
        public int MinGap { get; set; } = 15;
        public int MaxHoursPerDay { get; set; } = 5;
        public int MinTimeFrame { get; set; } = 15;

        public ScheduleSetting()
        {
            // Calculate the last Sunday
            DateTime currentDate = DateTime.Now;
            DateTime lastSunday = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            while (lastSunday.DayOfWeek != DayOfWeek.Sunday)
            {
                lastSunday = lastSunday.AddDays(-1);
            }

            // Set the StartHour to last Sunday at 9:00:00
            StartHour = new DateTime(lastSunday.Year, lastSunday.Month, lastSunday.Day, 9, 0, 0);

            // Calculate the upcoming Saturday
            DateTime upcomingSaturday = lastSunday.AddDays(6);

            // Set the EndHour to the upcoming Saturday at 18:00:00
            EndHour = new DateTime(upcomingSaturday.Year, upcomingSaturday.Month, upcomingSaturday.Day, 18, 0, 0);
        }
    }
    

}
