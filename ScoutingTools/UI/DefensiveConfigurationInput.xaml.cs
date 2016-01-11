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
    /// Interaction logic for DefensiveConfigurationInput.xaml
    /// </summary>
    public partial class DefensiveConfigurationInput : Window, INotifyPropertyChanged
    {
        public IReadOnlyDictionary<DefenseType, string> CategoryA => new Dictionary<DefenseType, string>()
        {
            {DefenseType.Portcullis, "Portcullis" },
            {DefenseType.ChevalDeFrise, "Checal De Frise" }
        }; 

        public IReadOnlyDictionary<DefenseType, string> CategoryB => new Dictionary<DefenseType, string>()
        {
            {DefenseType.Moat, "Moat" },
            {DefenseType.Ramparts, "Ramparts" }
        }; 

        public IReadOnlyDictionary<DefenseType, string> CategoryC => new Dictionary<DefenseType, string>()
        {
            {DefenseType.Drawbridge, "Draw Bridge" },
            {DefenseType.SallyPort, "Sally Port" }
        }; 

        public IReadOnlyDictionary<DefenseType, string> CategoryD => new Dictionary<DefenseType, string>()
        {
            {DefenseType.RockWall, "Rock Wall" },
            {DefenseType.RoughTerrain, "Rough Terrain" }
        };

        public event Action<DefenseConfiguration> ConfigurationCreated;

        public DefensiveConfigurationInput()
        {
            InitializeComponent();

            Grid.DataContext = this;
            CommitButton.Click += CommitButtonClicked;
        }

        private void CommitButtonClicked(object sender, RoutedEventArgs e)
        {
            if (CategoryADefenseSelection.SelectedIndex < 0 || CategoryBDefenseSelection.SelectedIndex < 0 ||
                CategoryCDefenseSelection.SelectedIndex < 0 || CategoryDDefenseSelection.SelectedIndex < 0)
            {
                return;
            }

            var categoryA = ((KeyValuePair<DefenseType, string>) CategoryADefenseSelection.SelectedItem).Key;
            var categoryB = ((KeyValuePair<DefenseType, string>) CategoryBDefenseSelection.SelectedItem).Key;
            var categoryC = ((KeyValuePair<DefenseType, string>) CategoryCDefenseSelection.SelectedItem).Key;
            var categoryD = ((KeyValuePair<DefenseType, string>) CategoryDDefenseSelection.SelectedItem).Key;
            
            ConfigurationCreated?.Invoke(new DefenseConfiguration()
            {
                SlotOne = DefenseType.LowBar,
                SlotTwo = categoryA,
                SlotThree = categoryB,
                SlotFour =  categoryC,
                SlotFive = categoryD,
                Key = DateTime.Now.ToLongDateString()
            });
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
