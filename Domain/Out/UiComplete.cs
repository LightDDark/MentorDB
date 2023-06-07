using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Out {
    public class UiComplete {
            public List<UiMission> missions { get; set; }
            public ScheduleSetting settings { get; set; }

            public UiComplete(ScheduleSetting scheduleSetting, List<UiMission> missionList)
            {
                settings = scheduleSetting;
                missions = missionList;
            }
        

    }
}
