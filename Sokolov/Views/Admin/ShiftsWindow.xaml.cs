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
using Sokolov.Models;
using Microsoft.EntityFrameworkCore;

namespace Sokolov.Views.Admin
{
    /// <summary>
    /// Interaction logic for ShiftsWindow.xaml
    /// </summary>
    public partial class ShiftsWindow : Window
    {
        private readonly SokolovContext _context;
        private AddShiftWindow _addShiftWindow;

        public ShiftsWindow()
        {
            InitializeComponent();
            _context = new SokolovContext();
            LoadShiftsAsync();
        }

        public List <Shift> Shifts { get; set;} = new List<Shift>();
        
        private async Task LoadShiftsAsync()
        {
            try
            {
                var shifts = await _context.Shifts.Include(s => s.ShiftUsers).ThenInclude(s => s.User).OrderByDescending(s => s.StartShift).ToListAsync();
                ShiftsGrid.ItemsSource = shifts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackAdminWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void AddNewShiftBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_addShiftWindow == null || !_addShiftWindow.IsVisible)
            {
                _addShiftWindow = new AddShiftWindow();
                _addShiftWindow.Closed += async (e, args) =>
                {
                    _addShiftWindow = null;
                    await LoadShiftsAsync();
                };
                _addShiftWindow.Show();
            }
            else
            {
                _addShiftWindow.Activate();
            }
        }
    }
}
