using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutingTools.Models.Enums;

namespace ScoutingTools.Models
{
    public class Alliance
    {
        /// <summary>
        /// Where the alliance was
        /// </summary>
        public string EventCode { get; set; }

        /// <summary>
        /// Match number of the alliance
        /// </summary>
        public int MatchNumber { get; set; }

        /// <summary>
        /// What type of match the alliance was in.
        /// </summary>
        public MatchType MatchType { get; set; }

        /// <summary>
        /// Sum of all points for the alliance for each robot event
        /// </summary>
        public int PointsCount { get; set; }

        /// <summary>
        /// Sum of all points given to other alliance
        /// </summary>
        public int PointsGiven { get; set; }

        /// <summary>
        /// All the robots in the match
        /// </summary>
        public ICollection<Team> Robots { get; set; }

        /// <summary>
        /// Robot events pertaining to alliance
        /// </summary>
        public ICollection<RobotEvent> RobotEvents { get; set; }

        /// <summary>
        /// Alliance Events for the match
        /// </summary>
        public ICollection<AllianceEvent> AllianceEvents { get; set; }

        /// <summary>
        /// The configuration that the alliance used for the match
        /// </summary>
        public DefenseConfiguration Defense { get; set; }

        /// <summary>
        /// What color the alliance was
        /// </summary>
        public AllianceColor Color { get; set; }
    }
}
