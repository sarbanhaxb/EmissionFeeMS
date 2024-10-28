using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Input;

namespace EmissionFeeMS.EmissionFeesMVVM

{
    /// <summary>
    /// Логика взаимодействия для EmissionFeesWindow.xaml
    /// </summary>
    public partial class EmissionFeesWindow : Window
    {

        public EmissionFeesWindow()
        {
            using ApplicationContext context = new ApplicationContext();
            context.EmissionFees.Load();

            InitializeComponent();
            MainData.ItemsSource = context.EmissionFees.Local.ToObservableCollection();

        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

        private void MinimizeApp(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }


}
