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

            for (int i = 0; (i < (int)(130.0 / capabilities.DefenseActionCost)) && (i < capabilities.CrossableSlots(defense) * 2); ++i)
            {
                total += 5;
            }

            double high = 130.0 / (2*capabilities.DefenseActionCost) * capabilities.ShootingPercentageHigh * 5;
            double low  = 130.0 / (2*capabilities.DefenseActionCost)* capabilities.ShootingPercentageLow;

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
