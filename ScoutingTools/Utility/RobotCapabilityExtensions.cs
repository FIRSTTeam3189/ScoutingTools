using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutingTools.Models;

namespace ScoutingTools.Utility
{
    public static class RobotCapabilityExtensions
    {
        public static int CrossableSlots(this RobotCapability cap, DefenseConfiguration defense)
        {
            return cap.DefensesCrossable.Count(x => defense.ContainedDefenses().Contains(x));
        }


    }
}
