using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingTools.Models.Enums
{
    public enum MatchPeriod
    {
        Autonomous,
        Teleop,
        Final
    }

    public class MatchPeriodUI
    {
        public static readonly IReadOnlyDictionary<MatchPeriod, string> UIStrings = new Dictionary<MatchPeriod, string>()
        {
            {MatchPeriod.Autonomous, "Autonomous" }, { MatchPeriod.Teleop, "Teleoperated" }, {MatchPeriod.Final, "Final" }
        };
    }
}
