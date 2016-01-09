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
    /// Interaction logic for RobotEventInput.xaml
    /// </summary>
    public partial class RobotEventInput : Window, INotifyPropertyChanged
    {
        public Alliance Alliance { get; set; }

        public RobotEventInput(Alliance alliance)
        {
            InitializeComponent();

            DataContext = this;
            Alliance = alliance;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
