using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScoutingTools.Models.Enums;
using ScoutingTools.Utility;

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
        [JsonIgnore]
        public int PointsCount {
            get
            {
                var goalPoints = RobotEvents.GetGoalPoints();
                var defensePoints = RobotEvents.GetCrossPoints();
                var challengePoints = RobotEvents.GetChallengePoints();
                var scalePoints = RobotEvents.GetScalePoints();
                var reachPoints = RobotEvents.GetReachPoints();

                return goalPoints + defensePoints + challengePoints + scalePoints + reachPoints;
            }
        }

        [JsonIgnore]
        public int ExtraRankingPoints => RobotEvents.GetExtraRankingPoints();

        /// <summary>
        /// Sum of all points given to other alliance
        /// </summary>
        [JsonIgnore]
        public int PointsGiven {
            get
            {
                // Let's add up the fouls by the robots and the alliance
                var robotFouls =
                    RobotEvents.Where(
                        x => x.Action == RobotActionType.Foul || x.Action == RobotActionType.TechnicalFoul);
                var allianceFouls =
                    AllianceEvents.Where(
                        x => x.Action == AllianceActionType.Foul || x.Action == AllianceActionType.TechnicalFoul);

                return robotFouls.Count() * GamePoints.Foul +
                       allianceFouls.Count() * GamePoints.Foul;
            }
        }

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
