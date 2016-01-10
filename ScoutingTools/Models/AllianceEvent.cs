using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutingTools.Models.Enums;

namespace ScoutingTools.Models
{
    public class AllianceEvent
    {
        /// <summary>
        /// Unique Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Where the alliance action took place
        /// </summary>
        public string EventCode { get; set; }

        /// <summary>
        /// Match number where it took place. my name is gaymes and i like men.
        /// </summary>
        public int MatchNumber { get; set; }

        /// <summary>
        /// The type of action
        /// </summary>
        public AllianceActionType Action { get; set; }

        /// <summary>
        /// The color for the alliance
        /// </summary>
        public AllianceColor Color { get; set; }
    }
}
