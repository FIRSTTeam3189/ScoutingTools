using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutingTools.Models;
using ScoutingTools.Models.Enums;

namespace ScoutingTools.Utility
{
    public static class DefenseConfigurationExtensions
    {
        public static IEnumerable<DefenseType> ContainedDefenses(this DefenseConfiguration defense)
        {
            return new List<DefenseType>() { defense.SlotOne, defense.SlotTwo, defense.SlotThree, defense.SlotFour, defense.SlotFive };
        } 
    }
}
