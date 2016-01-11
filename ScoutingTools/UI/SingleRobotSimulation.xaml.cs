using System;
using System.Collections.Generic;
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
    /// Interaction logic for SingleRobotSimulation.xaml
    /// </summary>
    public partial class SingleRobotSimulation : Window, INotifyPropertyChanged
    {
        public ICollection<Team> Teams { get; set; }

        public ICollection<DefenseConfiguration> Defenses { get; set; }

        public IReadOnlyDictionary<string, Func<double, Team, DefenseConfiguration>> Algorithms { get; set; }

        private Team _selectedTeam;
        public Team SelectedTeam {
            get { return _selectedTeam; }
            set
            {
                if (value != _selectedTeam)
                {
                    _selectedTeam = value;
                    OnPropertyChanged();
                }
            }
        }
        private DefenseConfiguration _selectedDefense;
        public DefenseConfiguration SeleDefense
        {
            get { return _selectedDefense; }
            set
            {
                if (value != _selectedDefense)
                {
                    _selectedDefense = value;
                    OnPropertyChanged();
                }
            }
        }

        private Func<double, Team, DefenseConfiguration> _selectedAlgorithm = null;

        public Func<double, Team, DefenseConfiguration> SelectedAlgorithm
        {
            get { return _selectedAlgorithm; }
            set
            {
                if (value != _selectedAlgorithm)
                {
                    _selectedAlgorithm = value;
                    OnPropertyChanged();
                }
            }
        }

        public SingleRobotSimulation()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
