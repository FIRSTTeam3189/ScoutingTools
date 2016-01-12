using System;
using System.Collections;
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
using ScoutingTools.Models.Enums;

namespace ScoutingTools.UI
{
    /// <summary>
    /// Interaction logic for LiveSingleRobotSimulation.xaml
    /// </summary>
    public partial class LiveSingleRobotSimulation : Window, INotifyPropertyChanged
    {
        public IReadOnlyDictionary<string, Func<Team, DefenseConfiguration, double>> Algorithms { get; set; }
        public IReadOnlyDictionary<DefenseType, string> Defenses => DefenseTypeUI.CapabilityUIStrings;
        public IReadOnlyDictionary<RobotCapabilityType, string> Capabilities => RobotCapabilityTypeUI.UIStrings;  
        public ObservableCollection<DefenseConfiguration> DefensiveConfigurations { get; set; }
        public string TeamName { get; set; }
        public int TeamNumber { get; set; }

        public LiveSingleRobotSimulation(IEnumerable<DefenseConfiguration> defenses, IReadOnlyDictionary<string, Func<Team, DefenseConfiguration, double>> algs)
        {
            InitializeComponent();
            DefensiveConfigurations = new ObservableCollection<DefenseConfiguration>(defenses);
            Algorithms = algs;
            Grid.DataContext = this;
            
        }

        private RobotCapability _robotCapability = new RobotCapability()
        {
            Abilities = new List<RobotCapabilityType>(),
            DefensesCrossable = new List<DefenseType>()
        };

        public RobotCapability RobotCapability {
            get { return _robotCapability; }
            set
            {
                if (RobotCapability != value)
                {
                    _robotCapability = value;
                    OnPropertyChanged();
                }
            }
        }

        private DefenseConfiguration _selectedDefense;

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

        private Func<Team, DefenseConfiguration, double> _selectedAlgorithm;

        public Func<Team, DefenseConfiguration, double> SelectedAlgorithm
        {
            get { return _selectedAlgorithm; }
            set
            {
                if (SelectedAlgorithm != value)
                {
                    _selectedAlgorithm = value;
                    OnPropertyChanged();

                }
            }
        }

        private const double Tol = .01;
        private double _calculatedValue;

        public double CalculatedValue
        {
            get { return _calculatedValue; }
            set
            {
                if (Math.Abs(_calculatedValue - value) > Tol)
                {
                    _calculatedValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedDefenses = DefensesSelected.SelectedItems.Cast<DefenseType>();
            var selectedCap = CapabilitiesSelected.SelectedItems.Cast<RobotCapabilityType>();

            RobotCapability.DefensesCrossable = selectedDefenses.ToList();
            RobotCapability.Abilities = selectedCap.ToList();

            var team = new Team()
            {
                Capabilities = RobotCapability, Name = TeamName, Number = TeamNumber
            };
            CalculatedValue = await Task.Run(() => SelectedAlgorithm(team, SelectedDefense));
        }

        private void DefenseSelection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDefense = DefenseSelection.SelectedIndex;
            if (selectedDefense < 0) return;

            SelectedDefense = DefensiveConfigurations[selectedDefense];
        }

        private void AlgorithmSelection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAlgorithm = AlgorithmSelection.SelectedIndex;
            if (selectedAlgorithm < 0) return;

            SelectedAlgorithm = Algorithms.ElementAt(selectedAlgorithm).Value;
        }
    }
}
