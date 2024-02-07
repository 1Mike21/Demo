using Microsoft.EntityFrameworkCore;
using Sokolov.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        private readonly SokolovContext _context;

        public EmployeesWindow()
        {
            InitializeComponent();

            _context = new SokolovContext();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                var employees = _context.Users.Include(u => u.Role).Where(u => u.Status == "Active").ToList();
                EmployeesGrid.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void DismissEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
           var selectedEmployee = EmployeesGrid.SelectedItem as User;

            if (selectedEmployee != null)
            {
                    selectedEmployee.Status = "Inactive";
                    _context.SaveChanges();
                    EmployeesGrid.Items.Remove(selectedEmployee);
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для увольнения.");
            }
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            var addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();
            LoadEmployees();
        }
        private void BackAdminWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();

        }
    }
}
