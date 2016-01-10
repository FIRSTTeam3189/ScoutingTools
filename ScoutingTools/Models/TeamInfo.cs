using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingTools.Models
{
    public class TeamInfo
    {
        public int Rank { get; set; }

        public double OffensiveRating { get; set; }

        public double DefensiveRating { get; set; }

        public double ActionsPerGame { get; set; }

        public int PointsScored { get; set; }

        public int PointsGiven { get; set; }
    }
}
