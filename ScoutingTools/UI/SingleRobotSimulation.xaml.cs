﻿using System;
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
    /// Interaction logic for SingleRobotSimulation.xaml
    /// </summary>
    public partial class SingleRobotSimulation : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Team> Teams { get; set; }

        public ObservableCollection<DefenseConfiguration> Defenses { get; set; }

        public IReadOnlyDictionary<string, Func<Team, DefenseConfiguration, double>> Algorithms { get; set; }

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
        public DefenseConfiguration SelectedDefense
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

        private Func<Team, DefenseConfiguration, double> _selectedAlgorithm = null;

        public Func<Team, DefenseConfiguration, double> SelectedAlgorithm
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

        private double _calculatedValue = 0.0;
        private const double Tolerance = .01;

        public double CalculatedValue
        {
            get { return _calculatedValue; }
            set
            {
                if (Math.Abs(value - _calculatedValue) > Tolerance)
                {
                    _calculatedValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public SingleRobotSimulation(IEnumerable<Team> teams, IEnumerable<DefenseConfiguration> configurations, IReadOnlyDictionary<string, Func<Team, DefenseConfiguration, double>> algoritms )
        {
            InitializeComponent();

            CalculateButton.Click += CalculateButtonOnClick;
            CalculateButton.IsEnabled = false;
            Teams = new ObservableCollection<Team>(teams);
            Defenses = new ObservableCollection<DefenseConfiguration>(configurations);
            Algorithms = algoritms;
            Grid.DataContext = this;
        }

        private async void CalculateButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            CalculateButton.IsEnabled = false;
            CalculatedValue = await Task.Run(() => SelectedAlgorithm(SelectedTeam, SelectedDefense));
            CalculateButton.IsEnabled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RobotSelection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = RobotSelection.SelectedIndex;
            if (index < 0)
            {
                SelectedTeam = null;
                CalculateButton.IsEnabled = false;
                return;
            }

            SelectedTeam = Teams[index];
            EnableCalculateIfCan();
        }

        private void DefenseSelection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = DefenseSelection.SelectedIndex;
            if (index < 0)
            {
                SelectedDefense = null;
                CalculateButton.IsEnabled = false;
                return;
            }

            SelectedDefense = Defenses[index];
            EnableCalculateIfCan();
        }

        private void AlgorithmSelection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = AlgorithmSelection.SelectedIndex;
            if (index < 0)
            {
                SelectedAlgorithm = null;
                CalculateButton.IsEnabled = false;
                return;
            }

            SelectedAlgorithm = Algorithms.ElementAt(index).Value;
            EnableCalculateIfCan();
        }

        private void EnableCalculateIfCan()
        {
            if (SelectedTeam != null && SelectedDefense != null && SelectedAlgorithm != null)
                CalculateButton.IsEnabled = true;
        }
    }
}
