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
using ScoutingTools.Models.Enums;

namespace ScoutingTools.UI
{
    /// <summary>
    /// Interaction logic for DefensiveConfigurations.xaml
    /// </summary>
    public partial class DefensiveConfigurations : Window, INotifyPropertyChanged
    {
        public ObservableCollection<DefenseConfiguration> DefenseConfigurations { get; set; }

        private DefenseConfiguration _selecetdDefense = null;

        public DefenseConfiguration SelectedDefense
        {
            get { return _selecetdDefense; }
            set
            {
                if (value != _selecetdDefense)
                {
                    _selecetdDefense = value;
                    OnPropertyChanged();
                }
            }
        }

        public DefensiveConfigurations(IEnumerable<DefenseConfiguration> defenses)
        {
            InitializeComponent();

            DefenseConfigurations = new ObservableCollection<DefenseConfiguration>(defenses);
            Grid.DataContext = this;
            AddDefense.Click += AddDefenseOnClick;
            EditDefense.IsEnabled = false;
        }

        private async void AddDefenseOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var input = new DefensiveConfigurationInput();
            var result = new TaskCompletionSource<DefenseConfiguration>();

            AddDefense.IsEnabled = false;
            input.Closed += (o, args) => { if (!result.Task.IsCompleted) result.SetResult(null); };
            input.ConfigurationCreated += configuration => result.SetResult(configuration);
            input.Show();
            var config = await result.Task;
            AddDefense.IsEnabled = true;

            if (config == null)
                return;

            Database.Instance.DefenseConfigurations.Add(config);
            DefenseConfigurations.Add(config);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DefensiveConfigurationsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DefensiveConfigurationsList.SelectedIndex < 0)
            {
                SelectedDefense = null;
                EditDefense.IsEnabled = false;
                return;
            }

            SelectedDefense = DefenseConfigurations[DefensiveConfigurationsList.SelectedIndex];
            EditDefense.IsEnabled = true;
        }
    }
}
