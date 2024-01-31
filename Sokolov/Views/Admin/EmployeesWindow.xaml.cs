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
        private ObservableCollection<User> _employees;

        public EmployeesWindow()
        {
            InitializeComponent();

            _context = new SokolovContext();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            _employees = new ObservableCollection<User>(_context.Users.Include(u => u.Role).Where(u => u.Status == "Active").ToList());
            EmployeesGrid.ItemsSource = _employees;
        }
        


        private void DismissEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem != null)
            {
                var selectedEmployee = EmployeesGrid.SelectedItem as User;

                using (var context = new SokolovContext())
                {
                    var employeeToUpdate = context.Users.FirstOrDefault(u => u.Id == selectedEmployee.Id);
                    if (employeeToUpdate != null)
                    {
                        employeeToUpdate.Status = "Inactive";
                        context.SaveChanges();
                        _employees.Remove(selectedEmployee);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для увольнения.");
            }
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.Show();
            this.Close();
        }
        private void BackAdminWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();

        }
    }
}
