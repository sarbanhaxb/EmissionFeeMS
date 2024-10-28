using System.Windows;
using System.Windows.Input;


namespace EmissionFeeMS.SettingsMVVM
{
    /// <summary>
    /// Логика взаимодействия для PropertyWindow.xaml
    /// </summary>
    public partial class MainSettings : Window
    {
        private readonly MainWindow? ParentWindow = null;
        private readonly Settings _settings;
        public MainSettings(MainWindow window)
        {
            InitializeComponent();
            ParentWindow = window;
            DataContext = new MainSettingsViewModel();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseApp(object sender, RoutedEventArgs e) => this.Close();

        private void MinimizeApp(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

        private void Save(object sender, RoutedEventArgs e)
        {
            _settings.InflationCoeff = Convert.ToDouble(InflationCoeff.Text.Replace('.', ','));
            _settings.IsInflationCoeff = Convert.ToBoolean(isInflationCoeff.IsChecked);
            _settings.NewCoeffAccept = Convert.ToBoolean(newCoeffAccept.IsChecked);
            _settings.IsMotivationAccept = Convert.ToBoolean(IsMotivationAccept.IsChecked);
            _settings.MotivatingCoeff = Convert.ToInt32(MotivatingCoeff.Text);
            _settings.SGNTcoeff = Convert.ToBoolean(SGNTcoeff.IsChecked);
        }
    }
}
