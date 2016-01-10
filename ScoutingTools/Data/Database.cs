using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using ScoutingTools.Models;
using ScoutingTools.Models.Enums;

namespace ScoutingTools.Data
{
    public class Database
    {
        static Database instance;

        public static Database Instance => instance;

        public static void Initialize()
        {
            instance = new Database();
        }

        public static void Initialize(string fromDirectory)
        {
            instance = new Database();
        }

        public ICollection<Team> Teams { get; private set; }

        public ICollection<RobotEvent> RobotEvents { get; private set; }

        public ICollection<DefenseConfiguration> DefenseConfigurations { get; private set; }

        public ICollection<AllianceEvent> AllianceEvents { get; private set; }

        public ICollection<Alliance> Alliances {
            get
            {
                var eventMatchGroup =
                    RobotEvents.GroupBy(x => x.MatchNumber).Select(x => new {ForMatch = x.Key, Event = x.ToList()});
                var allianceMatchGroup =
                    AllianceEvents.GroupBy(x => x.MatchNumber).Select(x => new {ForMatch = x.Key, Event = x.ToList()});
                var defenseConfigs = DefenseConfigurations.GroupBy(x => x.MatchNumber).Select(x => new { ForMatch = x.Key, Event = x.ToList() });
                var joinedEvents = from ev in eventMatchGroup
                                   join ev2 in allianceMatchGroup on ev.ForMatch equals ev2.ForMatch
                                   join ev3 in defenseConfigs on ev.ForMatch equals ev3.ForMatch
                                   select new { ForMatch = ev.ForMatch, RobotEvents = ev.Event, AllianceEvents = ev2.Event, DefenseConfigurations = ev3.Event };
                var alliances = new List<Alliance>();
                foreach (var ev in joinedEvents)
                {
                    if (ev.RobotEvents.Count == 0)
                        continue;
                    var matchType = ev.RobotEvents[0].MatchType;
                    var red = new Alliance()
                    {
                        MatchType = matchType,
                        AllianceEvents = ev.AllianceEvents.Where(x => x.Color == AllianceColor.Red).ToList(),
                        Color = AllianceColor.Red,
                        EventCode = ev.RobotEvents[0].EventCode,
                        RobotEvents = ev.RobotEvents.Where(x => x.AllianceColor == AllianceColor.Red).ToList(),
                        MatchNumber = ev.ForMatch,
                        Defense = ev.DefenseConfigurations.First(x => x.Color == AllianceColor.Red),
                        Robots = ev.RobotEvents.Where(x => x.AllianceColor == AllianceColor.Red).Select(x => x.Robot).Distinct().ToList()
                    };
                    var blue = new Alliance()
                    {
                        MatchType = matchType,
                        AllianceEvents = ev.AllianceEvents.Where(x => x.Color == AllianceColor.Blue).ToList(),
                        Color = AllianceColor.Blue,
                        EventCode = ev.RobotEvents[0].EventCode,
                        RobotEvents = ev.RobotEvents.Where(x => x.AllianceColor == AllianceColor.Blue).ToList(),
                        MatchNumber = ev.ForMatch,
                        Defense = ev.DefenseConfigurations.First(x => x.Color == AllianceColor.Blue),
                        Robots = ev.RobotEvents.Where(x => x.AllianceColor == AllianceColor.Blue).Select(x => x.Robot).Distinct().ToList()
                    };

                    alliances.Add(red);
                    alliances.Add(blue);
                }

                return alliances;
            }
        }

        private Database() {}
    }
}
