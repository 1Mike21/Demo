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
using System.Windows.Shapes;

namespace Sokolov.Admin
{
    /// <summary>
    /// Interaction logic for ShiftsWindow.xaml
    /// </summary>
    public partial class ShiftsWindow : Window
    {
        public ShiftsWindow()
        {
            InitializeComponent();
        }

        private void AddNewShift(object sender, RoutedEventArgs e)
        {
            AddShiftWindow addShiftWindow = new AddShiftWindow();
            addShiftWindow.Show();
            this.Close();
        }
    }
}
