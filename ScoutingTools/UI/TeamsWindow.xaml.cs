using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ScoutingTools.Data;
using ScoutingTools.Models;

namespace ScoutingTools.UI
{
    /// <summary>
    /// Interaction logic for TeamsWindow.xaml
    /// </summary>
    public partial class TeamsWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Team> Teams { get; set; }

        private Team _selectedTeam = null;
        public Team SelectedTeam {
            get
            {
                return _selectedTeam;
            }
            set
            {
                if (_selectedTeam != value)
                {
                    _selectedTeam = value;
                    OnPropertyChanged("SelectedTeam");
                }
            }
        }

        private TeamInfo _selectedTeamInfo = null;

        public TeamInfo SelectedTeamInfo
        {
            get { return _selectedTeamInfo; }
            set
            {
                if (_selectedTeamInfo != value)
                {
                    _selectedTeamInfo = value;
                    OnPropertyChanged("SelectedTeamInfo");
                }
            }
        }

        public TeamsWindow(IEnumerable<Team> teams)
        {
            InitializeComponent();

            Teams = new ObservableCollection<Team>(teams.OrderBy(x => x.Number));
            TeamsList.SelectionChanged += TeamsList_SelectionChanged;
            AddTeamButton.Click += AddTeamButtonOnClick;
            EditTeamButton.Click += EditTeamButtonOnClick;
            CapabilitiesButton.Click += CapabilitiesButtonOnClick;
            CapabilitiesButton.IsEnabled = false;
            Grid.DataContext = this;
        }

        private async void CapabilitiesButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            // Wait for a result from the capability window
            var result = new TaskCompletionSource<RobotCapability>();
            var capabilites = SelectedTeam.Capabilities != null ? new TeamCapabilityInput(SelectedTeam.Capabilities) : new TeamCapabilityInput();
            capabilites.RobotCapabilityCommited += capability => result.SetResult(capability);
            CapabilitiesButton.IsEnabled = false;
            capabilites.Show();

            var madeCapability        = await result.Task;
            SelectedTeam.Capabilities = madeCapability;
            CapabilitiesButton.IsEnabled = true;
        }

        private async void EditTeamButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var oldTeam = SelectedTeam;
            if (oldTeam == null)
                return;

            var input = new TeamInput(oldTeam);
            var result = new TaskCompletionSource<Team>();

            AddTeamButton.IsEnabled = false;
            input.Closed += (x, y) => { if (!result.Task.IsCompleted) result.SetResult(null); };
            input.TeamCreated += (t) => result.SetResult(t);
            input.Show();

            // Wait for the user to create the team
            var team = await result.Task;
            AddTeamButton.IsEnabled = true;
            if (team.Key != oldTeam.Key)
            {
                // Update the Database and UI
                Database.Instance.Teams.Remove(oldTeam);
                Database.Instance.Teams.Add(team);
                SelectedTeam = team;
                Teams.Remove(oldTeam);
                Teams.Add(team);
            }
        }

        private async void AddTeamButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var input = new TeamInput();
            var result = new TaskCompletionSource<Team>();

            AddTeamButton.IsEnabled = false;
            input.Closed += (x, y) => { if (!result.Task.IsCompleted) result.SetResult(null); };
            input.TeamCreated += (t) => result.SetResult(t);
            input.Show();

            // Wait for the user to create the team
            var team = await result.Task;
            AddTeamButton.IsEnabled = true;

            if (Database.Instance.AddTeam(team))
            {
                Teams.Add(team);
            }
        }

        private void TeamsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If we dont have a selected index
            if (TeamsList.SelectedIndex < 0)
            {
                SelectedTeam = null;
                CapabilitiesButton.IsEnabled = false;
                return;
            }
            SelectedTeam = Teams[TeamsList.SelectedIndex];
            CapabilitiesButton.IsEnabled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
