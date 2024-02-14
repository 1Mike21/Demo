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

namespace Sokolov.Views.Cook
{
    /// <summary>
    /// Interaction logic for CookWindow.xaml
    /// </summary>
    public partial class CookWindow : Window
    {
        private readonly SokolovContext _context;

        public CookWindow()
        {
            InitializeComponent();
            _context = new SokolovContext();
            LoadOrdersAsync();

            LogoutBtn.Click += (sender, e) =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            };
        }

        private async void LoadOrdersAsync()
        {
            var orders = await _context.Orders.Include(o => o.User).Include(o => o.OrderProducts).ThenInclude(op => op.Product).Where(o => o.Status == "Принят" || o.Status == "Готовится").ToListAsync();
            CurrentOrdersGrid.ItemsSource = orders;
        }

        private void ChangeStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = CurrentOrdersGrid.SelectedItem as Order;
            if (selectedOrder != null && selectedOrder.Status == "Принят")
            {
                selectedOrder.Status = "Готовится";
                _context.SaveChanges();
                MessageBox.Show("Статус заказа успешно сменен на \"Готовится\"", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadOrdersAsync();
            }
            else if (selectedOrder != null && selectedOrder.Status == "Готовится")
            {
                selectedOrder.Status = "Готов";
                _context.SaveChanges();
                MessageBox.Show("Статус заказа успешно сменен на \"Готов\"", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadOrdersAsync();
            }
            else
            {
                MessageBox.Show("Выберите заказ для смены статуса!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
