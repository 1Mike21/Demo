using Sokolov.Models;
using Sokolov.Views.Admin;
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
    /// Interaction logic for AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        private readonly SokolovContext _context;

        private Int32 user;

        public AddOrderWindow(Int32 currentUser = 1)
        {
            InitializeComponent();
            _context = new SokolovContext();
            this.user = currentUser;

            BackBtn.Click += (sender, e) =>
            {
                WaiterWindow waiterWindow = new WaiterWindow(this.user);
                waiterWindow.Show();
            };

            var products = _context.Products.ToList();
            ProductsListBox.ItemsSource = products;
        }

        private async void CreateOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedProducts = ProductsListBox.SelectedItems.Cast<Product>().ToList();

                var currentUser = _context.Users.FirstOrDefault(u => u.Id == this.user).Id;

                var place = PlaceTxb.Text;
                var countPerson = int.Parse(CountPersonTxb.Text);

                if (string.IsNullOrWhiteSpace(place) || string.IsNullOrWhiteSpace(CountPersonTxb.Text) || selectedProducts.Count == 0)
                {
                    MessageBox.Show("Заполните все поля.");
                    return;
                }

                var newOrder = new Order
                {
                    UserId = currentUser,
                    Place = place,
                    CountPerson = countPerson,
                    Status = "Принят",
                    Date = DateTime.Now,
                };

                foreach (var product in selectedProducts)
                {
                    newOrder.OrderProducts.Add(new OrderProduct { Product = product });
                }

                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();

                MessageBox.Show("Заказ успешно создан", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
