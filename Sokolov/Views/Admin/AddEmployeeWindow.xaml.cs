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

        public AddEmployeeWindow()
        {
            InitializeComponent();
            _context = new SokolovContext();
            LoadRoles();
        }

        private void LoadRoles()
        {
            try
            {
                RoleCmb.ItemsSource = _context.Roles.ToList();
                RoleCmb.DisplayMemberPath = "Name";

            }
            catch (Exception ex) 
            { 
                MessageBox.Show("Ошибка при загрузке ролей: " + ex.Message);
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameTxb.Text;
                string surname = SurnameTxb.Text;
                string login = LoginTxb.Text;
                string password = PasswordTxb.Password;
                Role selectedRole = RoleCmb.SelectedItem as Role;

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(login) 
                    || string.IsNullOrWhiteSpace(password) || selectedRole == null)
                {
                    MessageBox.Show("Заполните все поля.");
                    return;
                }

                User newUser = new User
                {
                    Name = name,
                    Surname = surname,
                    Login = login.ToLower(),
                    Password = password,
                    RoleId = selectedRole.Id,
                    Status = "Active",
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                EmployeeAdded?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Новый сотрудник успешно добавлен");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message);
            }
        }
    }
}
