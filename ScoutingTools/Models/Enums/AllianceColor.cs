﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingTools.Models.Enums
{
    public enum AllianceColor
    {
        Red, Blue
    }

    public static class AllianceColorUI
    {
        public static readonly IReadOnlyDictionary<AllianceColor, string> UIStrings = new Dictionary
            <AllianceColor, string>()
        {
            {AllianceColor.Blue, "Blue Alliance"},
            {AllianceColor.Red, "Red Alliance"}
        };
    }
   
}
