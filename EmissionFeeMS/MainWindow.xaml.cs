using Aspose.Words;
using EmissionFeeMS.EmissionFeesMVVM;
using EmissionFeeMS.SettingsMVVM;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmissionFeeMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            //NameScope.SetNameScope(ContxtMenu, NameScope.GetNameScope(this));

        }

        private void CloseApp(object sender, RoutedEventArgs e) => this.Close();

        private void MinimizeMaximizeApp(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.WindowState != WindowState.Maximized)
                {
                    //MinMaxIcon.Kind = PackIconKind.WindowRestore;

                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                    //MinMaxIcon.Kind = PackIconKind.WindowMaximize;
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

        //private void OpenFeeTaxes(object sender, RoutedEventArgs e) => new FeeTaxesWindow().ShowDialog();

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


        private void CalcResult(List<string[]> data)
        {
            //data.ForEach(item => calcResults.Add(new CalcResult() { Code = item[0], Mass = Convert.ToDouble(item[2]) }));
        }

        private void ClearDataGrid(object sender, RoutedEventArgs e)
        {
            //calcResults.Clear();
        }

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
            //if (MainData.CurrentCell.Column != null)
            //{
            //    switch (MainData.CurrentCell.Column.Header.ToString())
            //    {
            //        case "Mi, т":
            //            MassCell.IsReadOnly = false;
            //            break;
            //        case "Код вещества":
            //            var v = MainData.SelectedItem as CalcResult;
            //            CodeCell.IsReadOnly = false;
            //            break;
            //    }
            //}
        }

        private void MainData_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //MassCell.IsReadOnly = true;
            //CodeCell.IsReadOnly = true;
        }

        private void AddNewRow(object sender, RoutedEventArgs e)
        {
            //calcResults.Add(new CalcResult());
        }

        private void OpenPropertyWindows(object sender, RoutedEventArgs e)
        {
            //if ((bool)new PropertyWindow(this).ShowDialog())
            //{
            //    _settings = Settings.GetSettings();
            //    DataContext = _settings;
            //    ShowCoeffColumn();
            //}
        }

        private void IsChecked(object sender, RoutedEventArgs e)
        {
            //System.Windows.Controls.CheckBox senderCB = (System.Windows.Controls.CheckBox)sender;
            //switch (senderCB.Name)
            //{
            //    case "InflationCoeffCB":
            //        InflationCoeffColumn.Visibility = (bool)senderCB.IsChecked ? Visibility.Visible : Visibility.Hidden;
            //        foreach (CalcResult calcResult in calcResults)
            //        {
            //            calcResult.InflationCoeff = (bool)senderCB.IsChecked ? _settings.InflationCoeff : 1;
            //        }
            //        break;
            //    case "SGNTcoeffCB":
            //        SGNTcoeffColumn.Visibility = (bool)senderCB.IsChecked ? Visibility.Visible : Visibility.Hidden;
            //        foreach (CalcResult calcResult in calcResults)
            //        {
            //            calcResult.SGNTcoeff = (bool)senderCB.IsChecked ? 2 : 1;
            //        }
            //        break;
            //    case "MotivatingCoeffCB":
            //        MotivatingCoeffColumn.Visibility = (bool)senderCB.IsChecked ? Visibility.Visible : Visibility.Hidden;
            //        foreach (CalcResult calcResult in calcResults)
            //        {
            //            calcResult.MotivatingCoeff = (bool)senderCB.IsChecked ? new List<double>() { 25, 100 }[(bool)senderCB.IsChecked ? 1 : 0] : 1;
            //        }
            //        break;
            //}
        }

        private void FileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".xls",
                Filter = "Файл xls (*.xls)|*.xls|Файлы rtf(*.RTF)|*.RTF"
            };

            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result != false)
            {
                LoadFeeTable(openFileDialog.FileName);
            }
        }

        private void LoadFeeTable(string PATH)
        {
            if (System.IO.Path.GetExtension(PATH).ToUpperInvariant() == ".XLS")
            {
                //Workbook wb = new Workbook(PATH);
                //WorksheetCollection collestion = wb.Worksheets;
                //Worksheet ws = collestion[0];
                //double sum = 0;
                //int rows = ws.Cells.MaxDataRow;

                //for (int i = 0; i < rows; i++)
                //{
                //    if (SQLquery.CheckContainsPillutant(Convert.ToString(ws.Cells[i, 0].Value)))
                //    {
                //        string polNum = Convert.ToString(ws.Cells[i, 0].Value);
                //        string polTitle = Convert.ToString(ws.Cells[i, 1].Value);
                //        double polMass = Convert.ToDouble(ws.Cells[i, 6].Value);
                //        ResultTableElement El = new ResultTableElement();

                //        El.PollutantNumber = polNum;
                //        El.PollutantTitle = polTitle;
                //        El.PollutantMass = polMass;
                //        El.PollutantFee = SQLquery.GetFee(El.PollutantNumber);
                //        El.ExCoeff = Convert.ToInt32(ExCoefTextBox.Text);
                //        El.Coeff = Convert.ToDouble(ExYearCoefTextBox.Text);

                //        sum += El.Sum;

                //        resultTables.Add(El);
                //    }
                //}
                //TableFee.ItemsSource = resultTables;
                //MenuSaveCalc.IsEnabled = true;
                //SumTextBlock.Text = Convert.ToString(sum);
            }
            else if (System.IO.Path.GetExtension(PATH)
                                   .ToUpperInvariant() == ".RTF")
            {
                try
                {
                    Aspose.Words.Document document = new Aspose.Words.Document(PATH);
                    NodeCollection tables = document.GetChildNodes(NodeType.Table, true);
                    Aspose.Words.Tables.Table table = (Aspose.Words.Tables.Table)tables[0];
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string curCode = table.Rows[i].Cells[0].GetText().Trim('\a');
                        Regex regex = new(@"^\d{4}", RegexOptions.Multiline);
                        MatchCollection matches;
                        if (regex.IsMatch(curCode))
                        {
                            matches = regex.Matches(curCode);
                            foreach (Match match in matches)
                            {
                                string s = match.Value;
                                //calcResults.Add(new CalcResult() { Code = match.Value, Mass = Convert.ToDouble(table.Rows[i].Cells[3].GetText().Trim('\a').Replace('.', ',')) });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    new MessageBoxCustom($"Формат данных в файле не найден", MessageType.Error, MessageButtons.Ok).ShowDialog();
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenMainSettings(object sender, RoutedEventArgs e) => new MainSettings(this).ShowDialog();

    }
}