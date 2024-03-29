﻿using System;
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
using Microsoft.EntityFrameworkCore;
using Sokolov.Models;

namespace Sokolov.Views.Admin
{
    /// <summary>
    /// Interaction logic for OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        private readonly SokolovContext _context;

        public OrdersWindow()
        {
            InitializeComponent();
            _context = new SokolovContext();
            LoadOrdersAsync();
            backBtn.Click += (sender, e) =>
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                this.Close();
            };
        }

        public List<Order> Orders { get; set; } = new List<Order>();

        private async void LoadOrdersAsync()
        {
            Orders = await _context.Orders.Include(o => o.User).Include(o => o.OrderProducts).ThenInclude(op => op.Product).ToListAsync();
            OrdersGrid.ItemsSource = Orders;
        }
    }
}
