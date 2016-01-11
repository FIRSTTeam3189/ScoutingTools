using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutingTools.Models;
using ScoutingTools.Models.Enums;
using ScoutingTools.Utility;

namespace ScoutingTools.Algorithms
{
    public class SimulationAlgorithms
    {
        static readonly Random random = new Random();
        private const int Runs = 1000;

        private static int totalMisses = 0;
        private static int totalMakes = 0;

        public static double ScoreDetermilate(Team team, DefenseConfiguration defense)
        {
            var cap = team.Capabilities;
            var actions = cap.ActionPoints;
            var bestValue = 0.0;

            totalMisses = 0;
            totalMakes = 0;

            // Add the reach event if the robot can
            if (cap.Abilities.Contains(RobotCapabilityType.Reach))
                bestValue = 2;

            // If the robot cannot cross any defenses then just get all of the points that it can get
            if (cap.CrossableSlots(defense) < 1)
                return bestValue;

            // Grab the best calculated score
            var calculatedScores = new List<double>();
            for (int i = 0; i < cap.CrossableSlots(defense); ++i)
            {
                calculatedScores.Add(GetScoreWithCrossing(actions, cap.CrossableSlots(defense) - i,
                    cap.ShootingPercentageHigh, cap.ShootingPercentageLow, cap.DefenseActionCost, cap.ShootingActionCost));
            }
            bestValue += calculatedScores.Max();
            
            // Take into account the challenge and hang abilities
            if (cap.Abilities.Contains(RobotCapabilityType.Challenge))
                bestValue += GamePoints.Challenge*cap.ChallengePercentage;
            if (cap.Abilities.Contains(RobotCapabilityType.Scale))
                bestValue += GamePoints.Scale*cap.HungPercentage;

            // Finally take into account fouls
            bestValue -= GamePoints.Foul*cap.FoulPercentage;

            return bestValue;
        }

        /// <summary>
        /// Get Average score for a random set of events
        /// </summary>
        /// <param name="ap">AP points available</param>
        /// <param name="crossableSlots">Crossable Slots</param>
        /// <param name="highPercent">Percentage of High</param>
        /// <param name="lowPercent">Percentage of Low</param>
        /// <param name="crossCost">Cross Cost</param>
        /// <param name="shootCost">Shot Cost</param>
        /// <returns></returns>
        static double GetScoreWithCrossing(int ap, int crossableSlots, double highPercent,
            double lowPercent, int crossCost, int shootCost)
        {
            double runningAverage = 0;

            for (int i = 0; i < Runs; ++i)
            {
                runningAverage +=
                    GenerateRobotGoalEvents(ap, crossableSlots, highPercent, lowPercent, crossCost, shootCost)
                        .GetSoloRobotPointsCount()/(double) Runs;
            }

            return runningAverage;
        }

        /// <summary>
        /// Generates a pseuodorandom set of events based on parameters given
        /// </summary>
        /// <param name="ap">AP available</param>
        /// <param name="crossableSlots">Crossable slot</param>
        /// <param name="highPercent">High Shot percentage</param>
        /// <param name="lowPercent">Low Shot Percentage</param>
        /// <param name="crossCost">Cost per cross</param>
        /// <param name="shootCost">Cost per shot</param>
        /// <returns></returns>
        static ICollection<RobotEvent> GenerateRobotGoalEvents(int ap, int crossableSlots, double highPercent,
            double lowPercent, int crossCost, int shootCost)
        {
            var events = new List<RobotEvent>();

            // While we still have ap
            while (ap > 0 && ((ap - crossCost) > 0 || (ap - shootCost) > 0))
            {
                if (IsLastAMiss(events))
                {
                    // Generate 
                    AddShot(events, highPercent, lowPercent);
                    ap -= shootCost;
                }
                else if (IsLastACross(events))
                {
                    // Generate a shot event and deduct cross + shoot cost
                    AddShot(events, highPercent, lowPercent);
                    ap -= (crossCost + shootCost);
                }
                else
                {
                    // Add a cross 
                    AddCross(events, crossableSlots);
                    ap -= crossCost;
                }
            }

            totalMisses +=
                events.Count(
                    x => x.Action == RobotActionType.MissedHighGoal || x.Action == RobotActionType.MissedLowGoal);
            totalMakes +=
                events.Count(x => x.Action == RobotActionType.ShootIntoHigh || x.Action == RobotActionType.ShootIntoLow);

            return events;
        }

        /// <summary>
        /// Adds a cross, based on how many crossable slots there are
        /// </summary>
        /// <param name="events">Events to add to</param>
        /// <param name="crossableSlots">How many crossable slots there are</param>
        static void AddCross(List<RobotEvent> events, int crossableSlots)
        {
            var cross1Count = events.Count(x => x.Action == RobotActionType.CrossDefenseOne);
            var cross2Count = events.Count(x => x.Action == RobotActionType.CrossDefenseTwo) + (crossableSlots > 1 ? 0 : 100000);
            var cross3Count = events.Count(x => x.Action == RobotActionType.CrossDefenseThree) + (crossableSlots > 2 ? 0 : 100000);
            var cross4Count = events.Count(x => x.Action == RobotActionType.CrossDefenseFour) + (crossableSlots > 3 ? 0 : 100000);
            var cross5Count = events.Count(x => x.Action == RobotActionType.CrossDefenseFive) + (crossableSlots > 4 ? 0 : 100000);

            // Base add case
            if (cross1Count == 0)
            {
                events.Add(new RobotEvent() { Action = RobotActionType.CrossDefenseOne, MatchPeriod = MatchPeriod.Teleop });
                return;
            }

            // If Cross One is less than or equal to any of the other crosses
            if (cross1Count <= cross2Count && cross1Count <= cross3Count && cross1Count <= cross4Count &&
                cross1Count <= cross5Count )
            {
                events.Add(new RobotEvent() { Action = RobotActionType.CrossDefenseOne, MatchPeriod = MatchPeriod.Teleop });
                return;
            }

            // If Cross Two is less than or equal to any of the other crosses
            if (cross2Count <= cross3Count && cross2Count <= cross4Count && cross2Count <= cross5Count)
            {
                events.Add(new RobotEvent() { Action = RobotActionType.CrossDefenseTwo, MatchPeriod = MatchPeriod.Teleop });
                return;
            }

            // If Cross Three is less than or equal to any of the other crosses
            if (cross3Count <= cross4Count && cross3Count <= cross5Count)
            {
                events.Add(new RobotEvent() { Action = RobotActionType.CrossDefenseThree, MatchPeriod = MatchPeriod.Teleop });
                return;
            }

            // Finally...
            if (cross4Count <= cross5Count)
            {
                events.Add(new RobotEvent() { Action = RobotActionType.CrossDefenseFour, MatchPeriod = MatchPeriod.Teleop });
                return;
            }

            // Add a cross five if none of the other conditions come
            events.Add(new RobotEvent() { Action = RobotActionType.CrossDefenseFive, MatchPeriod = MatchPeriod.Teleop });
        }

        /// <summary>
        /// Adds a shot based on percentages
        /// </summary>
        /// <param name="events">Events to add to</param>
        /// <param name="shotHigh">Percentage of high shot</param>
        /// <param name="shotLow">Percentage of low shot</param>
        static void AddShot(List<RobotEvent> events, double shotHigh, double shotLow)
        {
            var ev = new RobotEvent() { MatchPeriod = MatchPeriod.Teleop };
            if (GamePoints.HighGoalTeleop*shotHigh > GamePoints.LowGoalTeleop*shotLow)
            {
                ev.Action = random.NextDouble() <= shotHigh
                    ? RobotActionType.ShootIntoHigh
                    : RobotActionType.MissedHighGoal;
            }
            else
            {
                ev.Action = random.NextDouble() <= shotLow
                    ? RobotActionType.ShootIntoLow
                    : RobotActionType.MissedLowGoal;
            }
            events.Add(ev);
        }

        /// <summary>
        /// Was the last event a cross
        /// </summary>
        /// <param name="events">Events to check</param>
        /// <returns>If last event was a cross</returns>
        static bool IsLastACross(List<RobotEvent> events)
        {
            if (events.Count == 0)
                return false;
            var last = events[events.Count - 1];
            return last.Action == RobotActionType.CrossDefenseOne || last.Action == RobotActionType.CrossDefenseTwo ||
                   last.Action == RobotActionType.CrossDefenseThree || last.Action == RobotActionType.CrossDefenseFour ||
                   last.Action == RobotActionType.CrossDefenseFive;
        }

        static bool IsLastAMiss(List<RobotEvent> events)
        {
            if (events.Count == 0)
                return false;

            var last = events[events.Count - 1];
            return last.Action == RobotActionType.MissedHighGoal || last.Action == RobotActionType.MissedLowGoal;
        }

        static bool IsLastAMade(List<RobotEvent> events)
        {
            if (events.Count == 0)
                return false;

            var last = events[events.Count - 1];
            return last.Action == RobotActionType.ShootIntoHigh || last.Action == RobotActionType.ShootIntoLow;
        }
    }
}
