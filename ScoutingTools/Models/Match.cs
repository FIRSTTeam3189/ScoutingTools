using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutingTools.Models.Enums;

namespace ScoutingTools.Models
{
    public class Match
    {
        public string Key { get; set; }

        public string EventCode { get; set; }

        public int MatchNumber { get; set; }

        public MatchType MatchType { get; set; }

        public int TotalPoints { get; set; }

        public Alliance RedAlliance { get; set; }

        public Alliance BlueAlliance { get; set; }
    }
}
