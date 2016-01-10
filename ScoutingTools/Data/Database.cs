using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using ScoutingTools.Models;
using ScoutingTools.Models.Enums;

namespace ScoutingTools.Data
{
    public class Database
    {
        private const string TeamsJson = "teams.json";
        private const string RoboteventsJson = "robotEvents.json";
        private const string DefenseconfigurationsJson = "defenseConfigurations.json";
        private const string AllianceeventsJson = "allianceEvents.json";
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

        /// <summary>
        /// All of the teams in the database
        /// </summary>
        public ICollection<Team> Teams { get; private set; }

        /// <summary>
        /// All of the total robot events that happened in the matches
        /// </summary>
        public ICollection<RobotEvent> RobotEvents { get; private set; }

        /// <summary>
        /// All of the configurations that were used for the matches
        /// </summary>
        public ICollection<DefenseConfiguration> DefenseConfigurations { get; private set; }

        /// <summary>
        /// All of the alliance events that happened in the matches
        /// </summary>
        public ICollection<AllianceEvent> AllianceEvents { get; private set; }

        /// <summary>
        /// All of the matches that happened
        /// </summary>
        public ICollection<Match> Matches {
            get
            {
                var groupedAlliances = Alliances.GroupBy(x => x.MatchNumber).Select(x => new {MatchNumber = x.Key, Alliences = x.ToList()});

                return groupedAlliances.Select(alliance => new Match
                {
                    BlueAlliance = alliance.Alliences.First(x => x.Color == AllianceColor.Blue), RedAlliance = alliance.Alliences.First(x => x.Color == AllianceColor.Red), MatchNumber = alliance.MatchNumber, MatchType = alliance.Alliences[0].MatchType
                }).ToList();
            }
        }

        /// <summary>
        /// All of the alliances in the game
        /// </summary>
        public ICollection<Alliance> Alliances {
            get
            {
                // Group The data by match number then join on matching number
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

        public async Task Save(string directory)
        {
            // Serialize the collections
            var teams = JsonConvert.SerializeObject(Teams);
            var robotEvents = JsonConvert.SerializeObject(RobotEvents);
            var defenseConfigurations = JsonConvert.SerializeObject(DefenseConfigurations);
            var allianceEvents = JsonConvert.SerializeObject(AllianceEvents);

            var directoryInfo = new DirectoryInfo(directory);
            if (!directoryInfo.Exists)
                directoryInfo.Create();

            // All of the file infos to save the json at
            var teamFile = new FileInfo(Path.Combine(directory, TeamsJson));
            var robotEventFile = new FileInfo(Path.Combine(directory, RoboteventsJson));
            var defenseConfigurationFile = new FileInfo(Path.Combine(directory, DefenseconfigurationsJson));
            var allianceEventFile = new FileInfo(Path.Combine(directory, AllianceeventsJson));

            // Write all of the data
            using (var writer = new StreamWriter(teamFile.OpenWrite()))
                await writer.WriteAsync(teams);

            using (var writer = new StreamWriter(robotEventFile.OpenWrite()))
                await writer.WriteAsync(robotEvents);

            using (var writer = new StreamWriter(defenseConfigurationFile.OpenWrite()))
                await writer.WriteAsync(defenseConfigurations);

            using (var writer = new StreamWriter(allianceEventFile.OpenWrite()))
                await writer.WriteAsync(allianceEvents);
        }

        public async Task Load(string directory)
        {
            // All of the file infos to load the json from
            var teamFile = new FileInfo(Path.Combine(directory, TeamsJson));
            var robotEventFile = new FileInfo(Path.Combine(directory, RoboteventsJson));
            var defenseConfigurationFile = new FileInfo(Path.Combine(directory, DefenseconfigurationsJson));
            var allianceEventFile = new FileInfo(Path.Combine(directory, AllianceeventsJson));

            string teamsJson = "";
            string robotEventsJson = "";
            string defenseConfigurationsJson = "";
            string allianceEventsJson = "";

            // Read in all the json
            using (var reader = new StreamReader(teamFile.OpenRead()))
                teamsJson = await reader.ReadToEndAsync();

            using (var reader = new StreamReader(robotEventFile.OpenRead()))
                robotEventsJson = await reader.ReadToEndAsync();

            using (var reader = new StreamReader(defenseConfigurationFile.OpenRead()))
                defenseConfigurationsJson = await reader.ReadToEndAsync();

            using (var reader = new StreamReader(allianceEventFile.OpenRead()))
                allianceEventsJson = await reader.ReadToEndAsync();

            Teams = JsonConvert.DeserializeObject<ICollection<Team>>(teamsJson);
            RobotEvents = JsonConvert.DeserializeObject<ICollection<RobotEvent>>(robotEventsJson);
            DefenseConfigurations =
                JsonConvert.DeserializeObject<ICollection<DefenseConfiguration>>(defenseConfigurationsJson);
            AllianceEvents = JsonConvert.DeserializeObject<ICollection<AllianceEvent>>(allianceEventsJson);
        }

        private Database()
        {
            Teams = new List<Team>();
            RobotEvents = new List<RobotEvent>();
            DefenseConfigurations = new List<DefenseConfiguration>();
            AllianceEvents = new List<AllianceEvent>();
        }
    }
}
