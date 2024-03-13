using Microsoft.EntityFrameworkCore;
using Sokolov.Models;
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

namespace Sokolov.Views.Waiter
{
    /// <summary>
    /// Interaction logic for WaiterWindow.xaml
    /// </summary>
    public partial class WaiterWindow : Window
    {
        private readonly SokolovContext _context;

        private Int32 user;

        public WaiterWindow(Int32 currentUser = 1)
        {
            InitializeComponent();
            _context = new SokolovContext();
            this.user = currentUser;
            LoadOrdersAsync();

            CreateOrderBtn.Click += (sender, e) =>
            {
                AddOrderWindow addOrderWindow = new AddOrderWindow(this.user);
                addOrderWindow.Show();
            };

            LogoutBtn.Click += (sender, e) =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            };
        }

        private async void LoadOrdersAsync()
        {
            var orders = await _context.Orders.Include(o => o.User).Include(o => o.OrderProducts).ThenInclude(o => o.Product).ToListAsync();
            CurrentOrdersGrid.ItemsSource = orders;
        }

        private void ChangeOrderStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = CurrentOrdersGrid.SelectedItem as Order;
            if (selectedOrder != null && selectedOrder.Status == "Готов")
            {
                selectedOrder.Status = "Оплачен";
                _context.SaveChanges();
                MessageBox.Show("Статус заказа успешно сменен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadOrdersAsync();
            }
            else if (selectedOrder.Status == "Готовится" || selectedOrder.Status == "Принят")
            {
                MessageBox.Show("Вы можете менять статус только для заказов со статусами \"Готов\"", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Выберите заказ для смены статуса!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
