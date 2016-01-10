using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingTools.Models.Enums
{
    public enum RobotCapabilityType
    {
        ShootHigh,
        ShootLow,
        Scale,
        Challenge,
        Reach
    }

    public static class RobotCapabilityTypeUI
    {
        public static readonly IReadOnlyDictionary<RobotCapabilityType, string>  UIStrings = new Dictionary<RobotCapabilityType, string>()
        {
            {RobotCapabilityType.ShootHigh, "Shoot High" },
            {RobotCapabilityType.ShootLow, "Shoot Low" },
            {RobotCapabilityType.Scale, "Scale Tower" },
            {RobotCapabilityType.Challenge, "Challenge" },
            {RobotCapabilityType.Reach, "Reach Defense" }
        }; 
    }
}
