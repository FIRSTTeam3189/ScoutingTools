using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutingTools.Models;
using ScoutingTools.Utility;

namespace ScoutingTools.Algorithms
{
    public class StatisticalAlgorithms
    {
        public static double ScoreCalculturminer(Team team, DefenseConfiguration defense)
        {
            var capabilities = team.Capabilities;
            var test = capabilities.CrossableSlots(defense);
            if (capabilities.CrossableSlots(defense) < 1)
            {
                return 0;
            }

            
            //total *= (.82 + (.02 * capabilities.DefensesCrossable.Count));

            double total = capabilities.HungPercentage * 15;
            total += capabilities.ChallengePercentage * 5;
            total += capabilities.CrossableSlots(defense) * 10;

            double high = 2 * capabilities.DefenseActionCost / 130.0 * capabilities.ShootingPercentageHigh * 5;
            double low  = 2 * capabilities.DefenseActionCost / 130.0 * capabilities.ShootingPercentageLow;

            if (high > low)
            {
                total += high;
            }
            else
            {
                total += low;
            }

            return total;
        }
    }
}
