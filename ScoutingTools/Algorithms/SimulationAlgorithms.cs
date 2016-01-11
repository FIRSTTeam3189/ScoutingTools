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
        public static double ScoreDetermilate(Team team, DefenseConfiguration defense)
        {
            var cap = team.Capabilities;
            var actions = cap.ActionPoints;
            var finalEvents = new List<RobotEvent>();
            
            // Add the reach event if the robot can
            if (cap.Abilities.Contains(RobotCapabilityType.Reach))
                finalEvents.Add(new RobotEvent() { Action = RobotActionType.ReachDefense, MatchPeriod = MatchPeriod.Autonomous, Robot = team });

            // If the robot cannot cross any defenses then just get all of the points that it can get
            if (cap.CrossableSlots(defense) < 1)
                return finalEvents.GetReachPoints();


            return 0;
        }
    }
}
