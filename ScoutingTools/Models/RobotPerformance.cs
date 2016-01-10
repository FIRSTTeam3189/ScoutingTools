using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingTools.Models
{
    public class RobotPerformance
    {
        public Team Team { get; set; }

        public int MatchNumber { get; set; }

        public ICollection<RobotEvent> Events { get; set; }

        public int PointsScored {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int PointsGiven
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int ExtraRankingPoints
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
