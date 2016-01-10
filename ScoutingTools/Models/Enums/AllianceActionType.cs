using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingTools.Models.Enums
{
    public enum AllianceActionType
    {
        Foul,
        TechnicalFoul
    }

    public class AllianceActionTypeUI
    {
        public static readonly IReadOnlyDictionary<AllianceActionType, string> UIStrings = new Dictionary
            <AllianceActionType, string>()
        {
            {AllianceActionType.Foul, "Foul"},
            {AllianceActionType.TechnicalFoul, "Techinal Foul"}
        };
    }
}
