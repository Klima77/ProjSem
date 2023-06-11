using Microsoft.EntityFrameworkCore;
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

namespace ProjSem
{
    /// <summary>
    /// Logika interakcji dla klasy WindowHistoria.xaml
    /// </summary>
    public partial class WindowHistoria : Window
    {
        private CarParkingContext dbContext;
        public WindowHistoria()
        {
            InitializeComponent();
            dbContext = new CarParkingContext();
            LoadHistoryData();
        }

        private void LoadHistoryData()
        {
            var currentDate = DateTime.Now;
            var pojazdy = dbContext.Pojazdies.Include("Bileties")
                .Where(p => p.Bileties.Any(b => b.DataWaznosci < currentDate))
                .ToList();

            dataGridHistoria.ItemsSource = pojazdy.SelectMany(p => p.Bileties).ToList();
        }
    }
}
