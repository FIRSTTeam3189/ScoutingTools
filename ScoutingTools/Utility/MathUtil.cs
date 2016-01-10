using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingTools.Utility
{
    public static class MathUtil
    {
        public static int Clamp(this int value, int low, int high)
        {
            return value < low ? low : (value > high ? high : value);
        }

        public static double Clamp(this int value, double low, double high)
        {
            return value < low ? low : (value > high ? high : value);
        }
    }
}
