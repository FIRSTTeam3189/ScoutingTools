using ScoutingTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScoutingTools.UI
{
    /// <summary>
    /// Interaction logic for TeamInput.xaml
    /// </summary>
    public partial class TeamInput : Window
    {
        public event Action<Team> TeamCreated;

        public TeamInput()
        {
            InitializeComponent();

            Create.Click += CreateOnClick;
            CancelButton.Click += (x, y) => Close();
        }

        public TeamInput(Team team) : this()
        {
            NameBox.Text = team.Name;
            NumberBox.Text = team.Number + "";
        }

        private void CreateOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!string.IsNullOrWhiteSpace(NameBox.Text) && !string.IsNullOrWhiteSpace(NumberBox.Text))
            {
                var t = new Team()
                {
                    Key = NameBox.Text + ":" + NumberBox.Text,
                    Number = int.Parse(NumberBox.Text),
                    Name = NameBox.Text
                };

                TeamCreated?.Invoke(t);
                this.Close();
            }
        }

        private void NumberBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsOnlyNumbers(NumberBox.Text);
        }

        bool IsOnlyNumbers(string text)
        {
            Regex reg= new Regex("[^0-9]+");
            return reg.IsMatch(text);
        }
    }
}
