using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutingTools.Models.Enums;

namespace ScoutingTools.Models
{
    public class DefenseConfiguration
    {
        /// <summary>
        /// Unique Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Event Code for where the configuration was
        /// </summary>
        public string EventCode { get; set; }

        /// <summary>
        /// Match number where the defense was in place
        /// </summary>
        public int MatchNumber { get; set; }

        /// <summary>
        /// String to show in UI
        /// </summary>
        public string UIString => MatchNumber + " : " + Color.ToString();

        /// <summary>
        /// Type of match where the defense took place
        /// </summary>
        public MatchType MatchType { get; set; }

        /// <summary>
        /// Color of the alliance
        /// </summary>
        public AllianceColor Color { get; set; }

        /// <summary>
        /// Defense in slot one
        /// </summary>
        public DefenseType SlotOne { get; set; }

        public string SlotOneUI => DefenseTypeUI.UIString[SlotOne];

        /// <summary>
        /// Defense in slot two
        /// </summary>
        public DefenseType SlotTwo { get; set; }

        public string SlotTwoUI => DefenseTypeUI.UIString[SlotTwo];

        /// <summary>
        /// Defense in slot three
        /// </summary>
        public DefenseType SlotThree { get; set; }

        public string SlotThreeUI => DefenseTypeUI.UIString[SlotThree];

        /// <summary>
        /// Defense in slot four
        /// </summary>
        public DefenseType SlotFour { get; set; }

        public string SlotFourUI => DefenseTypeUI.UIString[SlotFour];

        /// <summary>
        /// Defense in slot five
        /// </summary>
        public DefenseType SlotFive { get; set; }

        public string SlotFiveUI => DefenseTypeUI.UIString[SlotFive];
    }
}
