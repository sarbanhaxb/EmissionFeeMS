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
        }

        private void CollapseWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FullScreenWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.MinMaxImage.Source = (ImageSource)this.Resources["NormalScreenIconDrawingImage"];
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.MinMaxImage.Source = (ImageSource)this.Resources["FullScreenIconDrawingImage"];
                this.WindowState = WindowState.Normal;
            }
        }




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