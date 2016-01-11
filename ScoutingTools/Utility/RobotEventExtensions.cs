using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScoutingTools.Models;
using ScoutingTools.Models.Enums;

namespace ScoutingTools.Utility
{
    public static class RobotEventExtensions
    {
        /// <summary>
        /// Gets the extra ranking points from the match
        /// </summary>
        /// <param name="events">The events to get ranking points</param>
        /// <returns>The ranking points</returns>
        public static int GetExtraRankingPoints(this ICollection<RobotEvent> events)
        {
            int rp = 0;
            if (events.GetDefensesDamaged() >= GamePoints.BreachesBeforeRanking)
                rp += 1;
            if (events.IsTowerDefenseDown() && events.Count(x => x.Action == RobotActionType.Challenge) >= 3)
                rp += 1;

            return rp;
        }

        private static int GetDefensesDamaged(this ICollection<RobotEvent> events)
        {
            return events.Count(x => x.Action == RobotActionType.CrossDefenseOne).Clamp(0, 2)/2 +
                   events.Count(x => x.Action == RobotActionType.CrossDefenseTwo).Clamp(0, 2)/2 +
                   events.Count(x => x.Action == RobotActionType.CrossDefenseThree).Clamp(0, 2)/2 +
                   events.Count(x => x.Action == RobotActionType.CrossDefenseFour).Clamp(0, 2)/2 +
                   events.Count(x => x.Action == RobotActionType.CrossDefenseFive).Clamp(0, 2)/2;
        }

        /// <summary>
        /// Gets the total amount of points from goals
        /// </summary>
        /// <param name="events">Events to score</param>
        /// <returns>Points</returns>
        public static int GetGoalPoints(this ICollection<RobotEvent> events)
        {
            var highTeleopPoints = events.Count(
                    x => x.Action == RobotActionType.ShootIntoHigh &&
                        (x.MatchPeriod == MatchPeriod.Teleop || x.MatchPeriod == MatchPeriod.Final)) *
                                       GamePoints.HighGoalTeleop;
            var lowTeleopPoints = events.Count(
                x => x.Action == RobotActionType.ShootIntoLow &&
                    (x.MatchPeriod == MatchPeriod.Teleop || x.MatchPeriod == MatchPeriod.Final)) *
                                  GamePoints.LowGoalTeleop;
            var highAutoPoints = events.Count(
                x => x.Action == RobotActionType.ShootIntoHigh && x.MatchPeriod == MatchPeriod.Autonomous) *
                                 GamePoints.HighGoalAuto;
            var lowAutoPoints = events.Count(
                x => x.Action == RobotActionType.ShootIntoLow && x.MatchPeriod == MatchPeriod.Autonomous) *
                                GamePoints.LowGoalAuto;
            return highTeleopPoints + highAutoPoints + lowAutoPoints + lowTeleopPoints;
        }

        /// <summary>
        /// Gets total points scored by crossing defense
        /// </summary>
        /// <param name="events">The events to score</param>
        /// <returns>Points</returns>
        public static int GetCrossPoints(this ICollection<RobotEvent> events)
        {
            return events.PointsForCross(RobotActionType.CrossDefenseOne) +
                   events.PointsForCross(RobotActionType.CrossDefenseTwo) +
                   events.PointsForCross(RobotActionType.CrossDefenseThree) +
                   events.PointsForCross(RobotActionType.CrossDefenseFour) +
                   events.PointsForCross(RobotActionType.CrossDefenseFive);
        }

        /// <summary>
        /// Points by reaching defense in autonomous
        /// </summary>
        /// <param name="events">Events to score</param>
        /// <returns>Points</returns>
        public static int GetReachPoints(this ICollection<RobotEvent> events)
        {
            return
                events.Count(x => x.Action == RobotActionType.ReachDefense && x.MatchPeriod == MatchPeriod.Autonomous)
                    .Clamp(0, 3)*GamePoints.ReachDefenseAuto;
        }

        /// <summary>
        /// Points by scaling tower if its defense is down
        /// </summary>
        /// <param name="events">Events to score</param>
        /// <returns>Points</returns>
        public static int GetScalePoints(this ICollection<RobotEvent> events)
        {
            if (!events.IsTowerDefenseDown())
                return 0;

            return
                events.Count(x => x.Action == RobotActionType.Scale && x.MatchPeriod == MatchPeriod.Final).Clamp(0, 3)*
                GamePoints.Scale;
        }

        private static int PointsForCross(this ICollection<RobotEvent> events, RobotActionType crossType)
        {
            int autoCrosses = events.Count(x => x.Action == crossType && x.MatchPeriod == MatchPeriod.Autonomous).Clamp(0, 2);
            int teleopCrosses =
                events.Count(x => x.Action == crossType && x.MatchPeriod != MatchPeriod.Autonomous).Clamp(0, 2) -
                autoCrosses;

            return autoCrosses*GamePoints.CrossDefenseAuto + teleopCrosses*GamePoints.CrossDefenseTeleop;
        }

        /// <summary>
        /// The count of goals
        /// </summary>
        /// <param name="events">Events to count from</param>
        /// <returns></returns>
        public static int GetGoalCount(this ICollection<RobotEvent> events)
        {
            return
                events.Count(x => x.Action == RobotActionType.ShootIntoHigh || x.Action == RobotActionType.ShootIntoLow);
        }

        /// <summary>
        /// Score if the events are for a single robot. Not made for scoring alliances.
        /// </summary>
        /// <param name="events">The Events to score</param>
        /// <returns></returns>
        public static int GetSoloRobotPointsCount(this ICollection<RobotEvent> events)
        {
            return events.GetGoalCount() + events.GetCrossPoints() + events.GetReachPoints() +
                   (events.Count(x => x.Action == RobotActionType.Challenge) > 0 ? GamePoints.Challenge : 0) +
                   (events.Count(x => x.Action == RobotActionType.Scale) > 0 ? GamePoints.Scale : 0);
        }

        /// <summary>
        /// Checks if the tower defense is down
        /// </summary>
        /// <param name="events">The events to check over</param>
        /// <returns>If the tower defense is down</returns>
        public static bool IsTowerDefenseDown(this ICollection<RobotEvent> events)
        {
            return (GetGoalCount(events) - events.Count(x => x.Action == RobotActionType.TechnicalFoul)) >=
                   GamePoints.FullTowerStrength;
        }

        /// <summary>
        /// Gets the points scored by challenging the tower
        /// </summary>
        /// <param name="events">Events to score by</param>
        /// <returns>Points</returns>
        public static int GetChallengePoints(this ICollection<RobotEvent> events)
        {
            // We only get challenge points if the tower is taken down
            if (!events.IsTowerDefenseDown())
                return 0;

            return events.Count(x => x.Action == RobotActionType.Challenge).Clamp(0, 3)*GamePoints.Challenge;
        }
    }
}
