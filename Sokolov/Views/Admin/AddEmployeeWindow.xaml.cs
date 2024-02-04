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
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private readonly SokolovContext _context;
        public event EventHandler EmployeeAdded;

        private ObservableCollection<Role> _roles;


        public AddEmployeeWindow()
        {
            InitializeComponent();
            _context = new SokolovContext();
            LoadRoles();
        }

        private void LoadRoles()
        {
            _roles = new ObservableCollection<Role>(_context.Roles.ToList());
            RoleCmb.ItemsSource = _roles;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTxb.Text;
            string surname = SurnameTxb.Text;
            string login = LoginTxb.Text;
            string password = PasswordTxb.Password;
            string roleName = ((Role)RoleCmb.SelectedItem).Name.ToString();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(login) 
                || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(roleName))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            Role role = _context.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                MessageBox.Show("Выбранная роль не найдена.");
                return;
            }

            User newUser = new User
            {
                Name = name,
                Surname = surname,
                Login = login.ToLower(),
                Password = password,
                RoleId = role.Id,
                Status = "Active",
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            EmployeeAdded?.Invoke(this, EventArgs.Empty);
            MessageBox.Show("Новый сотрудник успешно добавлен");
            this.Close();

            EmployeesWindow employeesWindow = new EmployeesWindow();
            employeesWindow.Show();
        }
    }
}
