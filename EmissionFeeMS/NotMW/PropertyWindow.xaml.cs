using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace EmissionFeeMS.NotMW
{
    /// <summary>
    /// Логика взаимодействия для PropertyWindow.xaml
    /// </summary>
    public partial class PropertyWindow : Window
    {
        public PropertyWindow()
        {
            InitializeComponent();
            using ApplicationContext applicationContext = new ApplicationContext();
            applicationContext.COEFFICIENT.Load();

            InflationCoeff.Text = Convert.ToString(applicationContext.COEFFICIENT.Where(x => x.Title == "inflationCoeff").Select(x => x.Value).FirstOrDefault());
            SGNTcoeff.IsChecked = Convert.ToBoolean(applicationContext.COEFFICIENT.Where(x => x.Title == "SGNTcoeff").Select((x) => x.Value).FirstOrDefault());
            newCoeffAccept.IsChecked = Convert.ToBoolean(applicationContext.COEFFICIENT.Where(x => x.Title == "newCoeffAccept").Select((x) => x.Value).FirstOrDefault());
            ComboBoxItem comboBoxItem = new();
            switch (Convert.ToString(applicationContext.COEFFICIENT.Where(x => x.Title == "MotivatingCoeff").Select(x => x.Value).FirstOrDefault()))
            {
                case "25":
                    MotivatingCoeff.SelectedIndex = 0;
                    break;
                case "100":
                    MotivatingCoeff.SelectedIndex = 1;
                    break;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseApp(object sender, RoutedEventArgs e) => this.Close();

        private void MinimizeMaximizeApp(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.WindowState != WindowState.Maximized)
                {
                    MinMaxIcon.Kind = PackIconKind.WindowRestore;

                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                    MinMaxIcon.Kind = PackIconKind.WindowMaximize;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void MinimizeApp(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

        private void SaveDataChange(object sender, RoutedEventArgs e)
        {
            using ApplicationContext applicationContext = new ApplicationContext();
            applicationContext.COEFFICIENT.Load();

            var ELinflationCoeff = applicationContext.COEFFICIENT.Where(x => x.Title == "inflationCoeff").FirstOrDefault();
            var ELSGNTcoeff = applicationContext.COEFFICIENT.Where(x => x.Title == "SGNTcoeff").FirstOrDefault();
            var ELnewCoeffAccept = applicationContext.COEFFICIENT.Where(x => x.Title == "newCoeffAccept").FirstOrDefault();
            var ELMotivatingCoeff = applicationContext.COEFFICIENT.Where(x => x.Title == "MotivatingCoeff").FirstOrDefault();

            ELinflationCoeff.Value = InflationCoeff.Text;
            ELSGNTcoeff.Value = Convert.ToBoolean(SGNTcoeff.IsChecked) ? "true" : "false";
            ELnewCoeffAccept.Value = Convert.ToBoolean(newCoeffAccept.IsChecked) ? "true" : "false";
            ComboBoxItem v = MotivatingCoeff.SelectedItem as ComboBoxItem;
            ELMotivatingCoeff.Value = v.Content.ToString();
            applicationContext.Update(ELinflationCoeff);
            applicationContext.Update(ELSGNTcoeff);
            applicationContext.Update(ELnewCoeffAccept);
            applicationContext.Update(ELMotivatingCoeff);
            applicationContext.SaveChanges();

        }
    }
}
