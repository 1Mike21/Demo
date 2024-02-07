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

namespace Sokolov.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void ManageButton_Click<T>() where T : Window, new()
        {
            var window = new T();
            window.Show();
            Close();
        }
        private void ManageEmployeesBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageButton_Click<EmployeesWindow>();
        }

        private void ManageOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageButton_Click<OrdersWindow>();
        }


        private void ManageShiftsBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageButton_Click<ShiftsWindow>();
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageButton_Click<MainWindow>();
        }
    }
}
