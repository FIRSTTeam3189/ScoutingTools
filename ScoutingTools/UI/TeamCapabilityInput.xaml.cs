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
using ScoutingTools.Models.Enums;

namespace ScoutingTools.UI
{
    /// <summary>
    /// Interaction logic for TeamCapabilityInput.xaml
    /// </summary>
    public partial class TeamCapabilityInput : Window, INotifyPropertyChanged
    {
        public IReadOnlyDictionary<RobotCapabilityType, string> Capabilities => RobotCapabilityTypeUI.UIStrings;

        public IReadOnlyDictionary<DefenseType, string> Defenses => DefenseTypeUI.CapabilityUIStrings;

        public double HighPercentage { get; set; }

        public double LowPercentage { get; set; }

        public double HangPercentage { get; set; }

        public double FoulPercentage { get; set; }

        public double ChallengePercentage { get; set; }

        public int ActionPoints { get; set; }

        public int ShootingActionCost { get; set; }

        public int DefensiveActionCost { get; set; }

        public event Action<RobotCapability> RobotCapabilityCommited;

        public TeamCapabilityInput()
        {
            InitializeComponent();

            Grid.DataContext = this;

            CancelButton.Click += (sender, args) => Close();
            CommitButton.Click += CommitButtonOnClick;
        }

        public TeamCapabilityInput(RobotCapability capability) : this()
        {
            HighPercentage = capability.ShootingPercentageHigh;
            LowPercentage = capability.ShootingPercentageLow;
            FoulPercentage = capability.FoulPercentage;
            HangPercentage = capability.HungPercentage;
            ActionPoints = capability.ActionPoints;
            ShootingActionCost = capability.ShootingActionCost;
            DefensiveActionCost = capability.DefenseActionCost;
            ChallengePercentage = capability.ChallengePercentage;
        }

        private void CommitButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var selectedActions = SelectedCapabilities.SelectedItems.Cast<KeyValuePair<RobotCapabilityType, string>>().Select(x => x.Key).ToList();
            var selectedDefenses =
                SelectedDefenses.SelectedItems.Cast<KeyValuePair<DefenseType, string>>().Select(x => x.Key).ToList();

            var capability = new RobotCapability()
            {
                Abilities = selectedActions,
                DefensesCrossable = selectedDefenses,
                ActionPoints = ActionPoints,
                ShootingPercentageHigh = HighPercentage,
                ShootingPercentageLow = LowPercentage,
                FoulPercentage = FoulPercentage,
                HungPercentage = HangPercentage,
                DefenseActionCost = DefensiveActionCost,
                ShootingActionCost = ShootingActionCost,
                ChallengePercentage = ChallengePercentage
            };

            RobotCapabilityCommited?.Invoke(capability);
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
