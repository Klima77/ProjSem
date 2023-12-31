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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace ProjSem
{
    public partial class MainWindow : Window
    {
        private CarParkingContext dbContext;

        public MainWindow()
        {
            InitializeComponent();
            dbContext = new CarParkingContext();
            LoadPojazdyData();
        }

        private void LoadPojazdyData()
        {
            var currentDate = DateTime.Now;
            var pojazdy = dbContext.Pojazdies.Include("Bileties")
                .Where(p => p.Bileties.Any(b => b.DataWaznosci >= currentDate))
                .ToList();

            dataGridPojazdy.ItemsSource = pojazdy.SelectMany(p => p.Bileties).ToList();
        }

        private void btnAddCar_Click(object sender, RoutedEventArgs e)
        {
            string numerRejestracyjny = AddNrRej.Text;
            string rodzajBiletu = ((ComboBoxItem)WyborBiletu.SelectedItem)?.Content.ToString();

            if (!string.IsNullOrEmpty(numerRejestracyjny) && !string.IsNullOrEmpty(rodzajBiletu))
            {
                RodzajeBiletow rodzaj = dbContext.RodzajeBiletows.FirstOrDefault(r => r.Nazwa == rodzajBiletu);

                    // Tworzenie nowego obiektu pojazdu
                    Pojazdy nowyPojazd = new Pojazdy
                    {
                        NrRejestracyjny = numerRejestracyjny
                    };

                // Dzienny
                if (rodzajBiletu == "Dzienny")
                {
                    Bilety nowyBilet = new Bilety
                    {
                        NrRejestracyjny = numerRejestracyjny,
                        DataZakupu = DateTime.Now,
                        DataWaznosci = DateTime.Now.AddDays(1),
                        RodzajBiletu = rodzaj.Nazwa
                    };
                    nowyBilet.NrRejestracyjnyNavigation = nowyPojazd;
                    nowyBilet.RodzajBiletuNavigation = rodzaj;

                    dbContext.Pojazdies.Add(nowyPojazd);
                    dbContext.Bileties.Add(nowyBilet);
                    dbContext.SaveChanges();
                }
                // Godzinny
                else if (rodzajBiletu == "Godzinny")
                {
                    Bilety nowyBilet = new Bilety
                    {
                        NrRejestracyjny = numerRejestracyjny,
                        DataZakupu = DateTime.Now,
                        DataWaznosci = DateTime.Now.AddHours(1),
                        RodzajBiletu = rodzaj.Nazwa
                    };
                    nowyBilet.NrRejestracyjnyNavigation = nowyPojazd;
                    nowyBilet.RodzajBiletuNavigation = rodzaj;

                    dbContext.Pojazdies.Add(nowyPojazd);
                    dbContext.Bileties.Add(nowyBilet);
                    dbContext.SaveChanges();
                }

                // Miesieczny
                else if (rodzajBiletu == "Miesięczny")
                {
                    Bilety nowyBilet = new Bilety
                    {
                        NrRejestracyjny = numerRejestracyjny,
                        DataZakupu = DateTime.Now,
                        DataWaznosci = DateTime.Now.AddMonths(1),
                        RodzajBiletu = rodzaj.Nazwa
                    };
                    nowyBilet.NrRejestracyjnyNavigation = nowyPojazd;
                    nowyBilet.RodzajBiletuNavigation = rodzaj;

                    dbContext.Pojazdies.Add(nowyPojazd);
                    dbContext.Bileties.Add(nowyBilet);
                    dbContext.SaveChanges();
                }

                MessageBox.Show("Pojazd został dodany do bazy danych.");
            }
            else
            {
                MessageBox.Show("Proszę uzupełnić numer rejestracyjny oraz wybrać rodzaj biletu.");
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadPojazdyData();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void BtnHistoria_Click(object sender, RoutedEventArgs e)
        {
            WindowHistoria windowHistoria = new WindowHistoria();
            windowHistoria.ShowDialog();
        }
    }
}
