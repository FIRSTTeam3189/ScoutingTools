using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;
using Newtonsoft.Json;

namespace ScoutingTools.Models
{
    public class Team
    {
        public string Key { get; set; }
        
        public string Name { get; set; }

        public int Number { get; set; }

        public RobotCapability Capabilities { get; set; }

        [JsonIgnore]
        public int RankingPoints { get; set; }

        public string UIString => $"{Number} : {Name ?? ""}";

        public Brush UILabelColor => Capabilities == null ? Brushes.Red : Brushes.LightGreen;

        public ICollection<string> EventsAttended { get; set; }
    }
}
