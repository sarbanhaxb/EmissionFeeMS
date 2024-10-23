using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace EmissionFeeMS.NotMW
{
    /// <summary>
    /// Логика взаимодействия для FeeTaxesWindow.xaml
    /// </summary>
    /// 
    public partial class FeeTaxesWindow : Window
    {
        private readonly ObservableCollection<FeeTax> taxs;
        private readonly ApplicationContext context = new();

        public FeeTaxesWindow()
        {
            InitializeComponent();

            context.FeeTaxes.Load();
            taxs = context.FeeTaxes.Local.ToObservableCollection();
            MainData.ItemsSource = taxs;
        }

        private void CloseApp(object sender, RoutedEventArgs e) => Close();

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

        private void MinimizeApp(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void SaveDataChange(object sender, RoutedEventArgs e) => context.SaveChanges();

        private void EditPollutant(object sender, MouseButtonEventArgs e)
        {
            if (MainData.CurrentCell.Column.Header.ToString() == "Вещества") 
                PullutantField.IsReadOnly = false;
        }

        private void MainData_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (MainData.CurrentItem is FeeTax el1)
            {
                try
                {
                    context.Update(el1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            PullutantField.IsReadOnly = true;
        }
    }
}
