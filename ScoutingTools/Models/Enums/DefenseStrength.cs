using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingTools.Models.Enums
{
    public enum DefenseStrength
    {
        Full,
        Half,
        None
    }

    public class DefenseStrengthUI
    {
        public static readonly IReadOnlyDictionary<DefenseStrength, string> UIStrings = new Dictionary<DefenseStrength, string>()
            {
                {DefenseStrength.Full, "Full Defense Strength"},
                {DefenseStrength.Half, "Half Defense Strength"},
                {DefenseStrength.None, "No Defense Strength"}
            };
    }
}
