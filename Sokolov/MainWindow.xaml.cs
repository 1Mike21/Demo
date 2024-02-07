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
using Sokolov.Models;
using Microsoft.EntityFrameworkCore;

namespace Sokolov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            //Получаем введенные данные из TextBox и PasswordBox;
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            using (var context = new SokolovContext())
            {
                var user = context.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == username && u.Password == password);

                if (user!=null)
                {
                    switch (user.Role.Name)
                    {
                        case "Администратор":
                            NavigateToWindow(new Views.Admin.AdminWindow());
                            break;

                        case "Повар":
                            NavigateToWindow(new Views.Cook.CookWindow());
                            break;

                        case "Официант":
                            NavigateToWindow(new Views.Waiter.WaiterWindow());
                            break;

                        default:
                            MessageBox.Show("Неизвестная роль пользователя");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }

        private void NavigateToWindow(Window window)
        {
            window.Show();
            Close();
        }
    }
}
