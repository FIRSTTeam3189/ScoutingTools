using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingTools.Models.Enums
{
    public enum MatchType
    {
        Practice,
        Qualification,
        Playoff
    }

    public class MatchTypeUI
    {
        public static readonly IReadOnlyDictionary<MatchType, string> UIStrings = new Dictionary<MatchType, string>()
        {
            {MatchType.Practice, "Practice Match" }, {MatchType.Qualification, "Qualification Match" }, {MatchType.Playoff, "Playoff Match" }
        }
    }
}
