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
        MainWindow Parent;
        public PropertyWindow(MainWindow window)
        {
            InitializeComponent();

            Parent = window;

            InflationCoeff.Text = Convert.ToString(ApplicationProperty.AppProp.InflationCoeff);
            isInflationCoeff.IsChecked = ApplicationProperty.AppProp.IsInflationCoeff;
            newCoeffAccept.IsChecked = ApplicationProperty.AppProp.NewCoeffAccept;
            IsMotivationAccept.IsChecked = ApplicationProperty.AppProp.IsMotivationAccept;
            MotivatingCoeff.SelectedIndex = ApplicationProperty.AppProp.MotivatingCoeff;
            SGNTcoeff.IsChecked = ApplicationProperty.AppProp.SGNTcoeff;

            IsPrintedIfHaventFee.IsChecked = ApplicationProperty.AppProp.IsPrintedIfHaventFee;
            IsPrintedIfZero.IsChecked = ApplicationProperty.AppProp.IsPrintedIfZero;
            IsPrintedWithCoeff.IsChecked = ApplicationProperty.AppProp.IsPrintedWithCoeff;
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
            ApplicationProperty.AppProp.InflationCoeff = Convert.ToDouble(InflationCoeff.Text.Replace(".", ","));
            ApplicationProperty.AppProp.IsInflationCoeff = (bool)isInflationCoeff.IsChecked;
            ApplicationProperty.AppProp.NewCoeffAccept = (bool)newCoeffAccept.IsChecked;
            ApplicationProperty.AppProp.IsMotivationAccept = (bool)IsMotivationAccept.IsChecked;
            ApplicationProperty.AppProp.MotivatingCoeff = MotivatingCoeff.SelectedIndex;
            ApplicationProperty.AppProp.SGNTcoeff = (bool)SGNTcoeff.IsChecked;

            ApplicationProperty.AppProp.IsPrintedIfHaventFee = (bool)IsPrintedIfHaventFee.IsChecked;
            ApplicationProperty.AppProp.IsPrintedIfZero = (bool)IsPrintedIfZero.IsChecked;
            ApplicationProperty.AppProp.IsPrintedWithCoeff = (bool)IsPrintedWithCoeff.IsChecked;

            Parent.ShowCoeffColumn();
            ApplicationProperty.Save();
        }
    }
}
