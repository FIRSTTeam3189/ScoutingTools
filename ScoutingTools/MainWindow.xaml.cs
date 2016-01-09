using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScoutingTools.Models;
using ScoutingTools.UI;

namespace ScoutingTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            button.Click += (sender, args) =>
            {
                Alliance a = new Alliance() {Robots = new List<int>() {10, 20, 30, 40} };
                var win = new RobotEventInput(a);
                win.Show();
            };
        }
    }
}
