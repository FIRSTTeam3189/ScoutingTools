using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ScoutingTools.Models
{
    public class Team
    {
        public string Key { get; set; }
        
        public string Name { get; set; }

        public int Number { get; set; }

        public string UIString => $"{Number} : {Name ?? ""}";

        public ICollection<string> EventsAttended { get; set; }
    }
}
