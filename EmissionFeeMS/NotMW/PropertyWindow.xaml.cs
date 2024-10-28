using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.IO;
using System.Collections.ObjectModel;

namespace EmissionFeeMS.NotMW
{
    /// <summary>
    /// Логика взаимодействия для PropertyWindow.xaml
    /// </summary>
    public partial class PropertyWindow : Window
    {
        private readonly MainWindow? ParentWindow = null;
        private readonly Settings _settings;
        public PropertyWindow(MainWindow window)
        {
            InitializeComponent();
            _settings = Settings.GetSettings();
            _initControlls();
            ParentWindow = window;
        }

        private void _initControlls()
        {
            InflationCoeff.Text = Convert.ToString(_settings.InflationCoeff);
            isInflationCoeff.IsChecked = _settings.IsInflationCoeff == Visibility.Visible? true : false;
            newCoeffAccept.IsChecked = _settings.NewCoeffAccept;
            IsMotivationAccept.IsChecked = _settings.IsMotivationAccept == Visibility.Visible ? true : false;
            MotivatingCoeff.SelectedIndex = _settings.MotivatingCoeff;
            SGNTcoeff.IsChecked = _settings.SGNTcoeff == System.Windows.Visibility.Visible ? true : false;

            IsPrintedIfHaventFee.IsChecked = _settings.IsPrintedIfHaventFee;
            IsPrintedIfZero.IsChecked = _settings.IsPrintedIfZero;
            IsPrintedWithCoeff.IsChecked = _settings.IsPrintedWithCoeff;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseApp(object sender, RoutedEventArgs e) => this.Close();

        private void MinimizeApp(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

        private void SaveDataChange(object sender, RoutedEventArgs e)
        {
            _settings.InflationCoeff = Convert.ToDouble(InflationCoeff.Text.Replace(".", ","));
            _settings.IsInflationCoeff = (bool)isInflationCoeff.IsChecked ? Visibility.Visible: Visibility.Hidden;
            _settings.NewCoeffAccept = (bool)newCoeffAccept.IsChecked;
            _settings.IsMotivationAccept = (bool)IsMotivationAccept.IsChecked ? Visibility.Visible : Visibility.Hidden;
            _settings.MotivatingCoeff = MotivatingCoeff.SelectedIndex;
            _settings.SGNTcoeff = (bool)SGNTcoeff.IsChecked ? Visibility.Visible : Visibility.Hidden;

            _settings.IsPrintedIfHaventFee = (bool)IsPrintedIfHaventFee.IsChecked;
            _settings.IsPrintedIfZero = (bool)IsPrintedIfZero.IsChecked;
            _settings.IsPrintedWithCoeff = (bool)IsPrintedWithCoeff.IsChecked;

            ParentWindow.DataContext = _settings;
            _settings.Save();
        }
    }
}
