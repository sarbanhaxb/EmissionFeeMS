using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MaterialDesignThemes;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using EmissionFeeMS.NotMW;
using static MaterialDesignThemes.Wpf.Theme;
using System.Data;
using System.Text.RegularExpressions;


namespace EmissionFeeMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<CalcResult> calcResults = [];
        public MainWindow()
        {
            using ApplicationContext context = new();
            //using ApplicationContextOLD contextOLD = new ApplicationContextOLD();
            context.FeeTaxes.Load();
            //contextOLD.feetaxes.Load();

            //ObservableCollection<FeeTax> FT = context.FeeTaxes.Local.ToObservableCollection();
            //ObservableCollection<FeeTax> FTold = contextOLD.feetaxes.Local.ToObservableCollection();

            //foreach (FeeTax feeTax in FTold)
            //{
            //    Trace.WriteLine($"{feeTax.Pollutant} {feeTax.Title}");

            //    feeTax.Fee = Math.Round(feeTax.Fee, 2 );
            //    context.FeeTaxes.Add( feeTax );
            //    context.SaveChanges();
            //}

            InitializeComponent();
            MainData.ItemsSource = calcResults;

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

        private void OpenEmissionFees(object sender, RoutedEventArgs e) => new EmissionFeesWindow().ShowDialog();

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void OpenFeeTaxes(object sender, RoutedEventArgs e) => new FeeTaxesWindow().ShowDialog();

        private void PasteFromClipboard()
        {

            List<string[]> list = [];
            var clip = Clipboard.GetText().Replace("\r", "");
            string pattern = @"^\d{4}\t[А-Яа-я()a-zA-Z;/,0-9-:% ]+\t[а-яА-Я /\n]+\t[0-9,\ne-]+\t[0-9 ]\t[0-9,e-]+\t[0-9,]+\n";
            string altPattern = @"^\d{4}\t[А-Яа-я()a-zA-Z;/,0-9-:% ]+\t[0-9.]+\t[0-9.]+\n";

            Regex regex = new(pattern, RegexOptions.Multiline);
            Regex altRegex = new(altPattern, RegexOptions.Multiline);
            MatchCollection matches;

            if (regex.IsMatch(clip))
            {
                matches = regex.Matches(clip);
                foreach (Match match in matches)
                {
                    string[] s = match.Value.Split('\t');
                    list.Add([s[0], s[1], s[6]]);
                }
                CalcResult(list);
            }
            else if (altRegex.IsMatch(clip))
            {
                matches = altRegex.Matches(clip);
                foreach (Match match in matches)
                {
                    string[] s = match.Value.Split("\t");
                    list.Add([s[0], s[1], s[3].Replace('.', ',')]);
                }
                CalcResult(list);
            }
            else new MessageBoxCustom("Неизвестный формат данных", MessageType.Error, MessageButtons.Ok) { Owner = this }.ShowDialog();
        }


        private void CalcResult(List<string[]> data) => data.ForEach(item => calcResults.Add(new CalcResult() { Code = item[0], Mass = Convert.ToDouble(item[2]) }));

        private void PrintCacl(object sender, RoutedEventArgs e)
        {
        }

        private void AddSumCalc(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteItem(object sender, RoutedEventArgs e) => MainData.SelectedItems.OfType<CalcResult>().ToList().ForEach(item => calcResults.Remove(item));
        private void ClearDataGrid(object sender, RoutedEventArgs e) => calcResults.Clear();

        private void MainData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                PasteFromClipboard();
            }
            else if (e.Key == Key.C && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                new MessageBoxCustom("ТУТ ПОКА НИЧЕГО НЕТ", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
        }

        private void PasteButton(object sender, RoutedEventArgs e) => PasteFromClipboard();

        private void MainData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MainData.CurrentCell.Column != null)
            {
                Trace.WriteLine(MainData.CurrentCell.Column.Header.ToString());

                switch (MainData.CurrentCell.Column.Header.ToString())
                {
                    case "Mi, т":
                        MassCell.IsReadOnly = false;
                        break;
                    case "Код вещества":
                        var v = MainData.SelectedItem as CalcResult;
                        /*if (v.Code is null) */CodeCell.IsReadOnly = false;
                        break;
                }
            }
        }

        private void MainData_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            MassCell.IsReadOnly = true;
            CodeCell.IsReadOnly = true;
        }

        private void AddNewRow(object sender, RoutedEventArgs e)
        {
            calcResults.Add(new CalcResult());
        }

        private void OpenPropertyWindows(object sender, RoutedEventArgs e) => new PropertyWindow().ShowDialog();


        //public void LoadFromMS()
        //{
        //    Trace.WriteLine("TUT");

        //    string filePath = "C:\\Users\\sarba\\source\\repos\\EmissionFeeMS\\EmissionFeeMS\\TEMP(1).DOCX";
        //    using ApplicationContext context = new ApplicationContext();
        //    using WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false);
        //    context.Database.EnsureCreated();
        //    Body body = wordDoc.MainDocumentPart.Document.Body;
        //    IEnumerable<Table> tables = body.Elements<Table>();

        //    foreach (Table table in tables)
        //    {
        //        foreach (var row in table.Elements<TableRow>())
        //        {
        //            List<string> rowData = new List<string>();
        //            foreach (var cell in row.Elements<TableCell>())
        //            {
        //                string cellText = cell.InnerText;
        //                rowData.Add(cellText);
        //            }
        //            EmissionFee emissionFee = new EmissionFee();
        //            emissionFee.Code = rowData[0];
        //            emissionFee.Title = rowData[1];
        //            emissionFee.HazardClass = Convert.ToString(rowData[2]);
        //            context.EmissionFees.Add(emissionFee);
        //        }
        //    }
        //    context.SaveChanges();
        //    Trace.WriteLine("USPESHNIY USPEH");
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    LoadFromMS();
        //}
    }
}