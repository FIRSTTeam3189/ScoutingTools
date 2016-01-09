using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutingTools.Models.Enums;

namespace ScoutingTools.Models
{
    public class RobotEvent
    {
        /// <summary>
        /// Unique Key in DB
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Event code for where this RobotEvent took place
        /// </summary>
        public string EventCode { get; set; }

        /// <summary>
        /// The Robot number who made this event
        /// </summary>
        public int RobotNumber { get; set; }

        /// <summary>
        /// Match where this event took place
        /// </summary>
        public int MatchNumber { get; set; }

        /// <summary>
        /// If this event was a practice, qualification, or playoff match
        /// </summary>
        public MatchType MatchType { get; set; }

        /// <summary>
        /// Color of alliance they were on
        /// </summary>
        public AllianceColor AllianceColor { get; set; }

        /// <summary>
        /// The Scoring play
        /// </summary>
        public RobotActionType RobotAction { get; set; }

        /// <summary>
        /// If other was specified for the robot action
        /// </summary>
        public int OtherScoreAmount { get; set; }

        /// <summary>
        /// When the event took place in the match
        /// </summary>
        public MatchPeriod MatchPeriod { get; set; }
    }
}
