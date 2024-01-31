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

        private void ManageOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            OrdersWindow ordersWindow = new OrdersWindow();
            ordersWindow.Show();
            this.Close();
        }

        private void ManageEmployeesBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeesWindow employeeWindow = new EmployeesWindow();
            employeeWindow.Show();
            this.Close();

        }

        private void ManageShiftsBtn_Click(object sender, RoutedEventArgs e)
        {
            ShiftsWindow shiftsWindow = new ShiftsWindow();
            shiftsWindow.Show();
            this.Close();
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
