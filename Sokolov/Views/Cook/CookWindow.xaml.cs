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
        }

        public List<Order> Orders { get; set; } = new List<Order>();

        private async void LoadOrdersAsync()
        {
            Orders = await _context.Orders.Include(o => o.User).Include(o => o.OrderProducts).ThenInclude(op => op.Product).Where(o => o.Status == "Accept").ToListAsync();
            CurrentOrdersGrid.ItemsSource = Orders;
        }

        private void ChangeStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = CurrentOrdersGrid.SelectedItem as Order;
            if (selectedOrder != null && selectedOrder.Status == "")
            {
                selectedOrder.Status = 
            }
        }
    }
}
