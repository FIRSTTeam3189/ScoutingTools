using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingTools.Models.Enums
{
    public enum RobotActionType
    {
        CrossDefenseOne,
        CrossDefenseTwo,
        CrossDefenseThree,
        CrossDefenseFour,
        CrossDefenseFive,
        MissedHighGoal,
        MissedLowGoal,
        ShootIntoHigh,
        ShootIntoLow,
        Scale,
        Challenge,
        ReachDefense,
        Foul,
        TechnicalFoul,
        Other
    }

    public static class RobotActionTypeUI
    {
        public static readonly IReadOnlyDictionary<RobotActionType, string> UIStrings  = new Dictionary<RobotActionType, string>()
        {
            { RobotActionType.CrossDefenseOne, "Cross Defense One" },
            { RobotActionType.CrossDefenseTwo, "Cross Defense Two" },
            { RobotActionType.CrossDefenseThree, "Cross Defense Three" },
            { RobotActionType.CrossDefenseFour, "Cross Defense Four" },
            { RobotActionType.CrossDefenseFive, "Cross Defense Five" },
            { RobotActionType.MissedHighGoal, "Missed High" },
            { RobotActionType.MissedLowGoal, "Missed Low" },
            { RobotActionType.ShootIntoHigh, "Made High" },
            { RobotActionType.ShootIntoLow, "Made Low" },
            { RobotActionType.Scale, "Scale" },
            { RobotActionType.Challenge, "Challenge" },
            { RobotActionType.ReachDefense, "Reached Defense" },
            { RobotActionType.Foul, "Foul" },
            { RobotActionType.TechnicalFoul, "Technical Foul" },
            { RobotActionType.Other, "Other" }
        };
    }
}
