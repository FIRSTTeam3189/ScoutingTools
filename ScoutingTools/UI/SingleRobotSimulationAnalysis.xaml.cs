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
using ScoutingTools.Models;

namespace ScoutingTools.UI
{
    /// <summary>
    /// Interaction logic for SingleRobotSimulationAnalysis.xaml
    /// </summary>
    public partial class SingleRobotSimulationAnalysis : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Team> Teams { get; set; }

        private Func<Team, DefenseConfiguration, double> _algorithm;

        private DefenseConfiguration _selectedDefense = null;
        public DefenseConfiguration SelectedDefense
        {
            get { return _selectedDefense; }
            set {
                if (_selectedDefense != value)
                {
                    _selectedDefense = value;
                    OnPropertyChanged();
                }
            }
        }

        private Team _selectedTeam = null;
        public Team SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {
                if (_selectedTeam != value)
                {
                    _selectedTeam = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _selectedValue;
        private const double TOLERANCE = .01;

        public double SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                if (Math.Abs(_selectedValue - value) > TOLERANCE)
                {
                    _selectedValue = value;
                    OnPropertyChanged();
                }
            }
        }
         
        public SingleRobotSimulationAnalysis(IEnumerable<Team> teams, DefenseConfiguration defensive, Func<Team, DefenseConfiguration, double>  alg)
        {
            InitializeComponent();

            Teams = new ObservableCollection<Team>(teams);
            SelectedDefense = defensive;
            _algorithm = alg;
            Grid.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TeamSelection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamSelection.SelectedIndex < 0)
            {
                SelectedTeam = null;
                SelectedValue = 0;
                return;
            }

            SelectedTeam = Teams[TeamSelection.SelectedIndex];
            SelectedValue = _algorithm(SelectedTeam, SelectedDefense);
        }
    }
}
