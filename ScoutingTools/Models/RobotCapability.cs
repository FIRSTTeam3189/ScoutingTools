using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutingTools.Models.Enums;

namespace ScoutingTools.Models
{
    public class RobotCapability
    {
        /// <summary>
        /// All of the abilities of the Robot
        /// </summary>
        public ICollection<RobotCapabilityType> Abilities { get; set; }

        /// <summary>
        /// All of the Defenses crossable
        /// </summary>
        public ICollection<DefenseType> DefensesCrossable { get; set; }

        /// <summary>
        /// Shooting percentage of into the high goal
        /// </summary>
        public double ShootingPercentageHigh { get; set; }

        /// <summary>
        /// Action Points, amount of actions that a robot can take per match
        /// </summary>
        public int ActionPoints { get; set; }

        /// <summary>
        /// Defensive Action cost per crossing a defense
        /// </summary>
        public int DefenseActionCost { get; set; }

        /// <summary>
        /// Offensive action cost per doing offensive action, This affects shooting actions
        /// </summary>
        public int ShootingActionCost { get; set; }

        /// <summary>
        /// Missing chance into high goal
        /// </summary>
        public double MissPercentageHigh => 1 - ShootingPercentageHigh;

        /// <summary>
        /// Shooting percentage into low goal
        /// </summary>
        public double ShootingPercentageLow  { get; set; }

        /// <summary>
        /// Missing chance into low goal
        /// </summary>
        public double MissPercentageLow => 1 - ShootingPercentageLow;

        /// <summary>
        /// Chance of hanging at end of match
        /// </summary>
        public double HungPercentage { get; set; }

        /// <summary>
        /// Chance of Not hanging at end of match
        /// </summary>
        public double NotHungPercentage => 1 - HungPercentage;

        /// <summary>
        /// Chance of challenge at the end of the game
        /// </summary>
        public double ChallengePercentage { get; set; }

        /// <summary>
        /// Chance they won't challenge at the end of the game
        /// </summary>
        public double NoChallengePercentage => 1 - ChallengePercentage;

        /// <summary>
        /// Chance of fouling during match
        /// </summary>
        public double FoulPercentage { get; set; }

        /// <summary>
        /// Chance of not fouling during the match
        /// </summary>
        public double NoFoulPercentage => 1 - FoulPercentage;
    }
}
