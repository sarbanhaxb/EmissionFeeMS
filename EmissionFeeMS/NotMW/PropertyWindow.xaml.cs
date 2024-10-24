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
        public PropertyWindow()
        {
            InitializeComponent();
            
            DataContext = ApplicationProperty.AppProp;
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
            ApplicationProperty.Save();
        }
    }
}
