﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using ScoutingTools.Algorithms;
using ScoutingTools.Data;
using ScoutingTools.Models;
using ScoutingTools.UI;

namespace ScoutingTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Directory { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            SaveDatabaseButton.Click += async (sender, args) => await SaveData();
            LoadDatabaseButton.Click += async (sender, args) => await LoadData();
            InitializeDatabaseButton.Click += (sender, args) => InitData();
            ViewTeamsButton.Click += (sender, args) => (new TeamsWindow(Database.Instance.Teams)).Show();
            ViewDefenseConfigurationsButton.Click += (x, args) => (new DefensiveConfigurations(Database.Instance.DefenseConfigurations)).Show();
            SimulateData.Click += SimulateDataOnClick;
            SimulateLive.Click += SimulateLive_Click;
        }

        private void SimulateLive_Click(object sender, RoutedEventArgs e)
        {
            var def = Database.Instance.DefenseConfigurations;
            var algorithms = new Dictionary<string, Func<Team, DefenseConfiguration, double>>()
            {
                {"Score Calculturminer", StatisticalAlgorithms.ScoreCalculturminer },
                {"Score Determilate", SimulationAlgorithms.ScoreDetermilate }
            };
            var win = new LiveSingleRobotSimulation(def, algorithms);
            win.Show();
        }

        private void SimulateDataOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var teams = Database.Instance.Teams;
            var def = Database.Instance.DefenseConfigurations;
            var algorithms = new Dictionary<string, Func<Team, DefenseConfiguration, double>>()
            {
                {"Score Calculturminer", StatisticalAlgorithms.ScoreCalculturminer },
                {"Score Determilate", SimulationAlgorithms.ScoreDetermilate }
            };
            var window = new SingleRobotSimulation(teams, def, algorithms);
            window.Show();
        }

        public void SetDirectory()
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            Directory = dialog.SelectedPath;
        }

        public async Task SaveData()
        {
            if (string.IsNullOrWhiteSpace(Directory))
                SetDirectory();

            await Database.Instance.Save(Directory);
        }

        public void InitData()
        {
            Database.Initialize();
        }

        public async Task LoadData()
        {
            if (string.IsNullOrWhiteSpace(Directory))
                SetDirectory();

            await Database.Initialize(Directory);
        }
    }
}
