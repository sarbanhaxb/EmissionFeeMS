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
        private readonly Dictionary<string, dynamic> PropDict;
        public PropertyWindow()
        {
            InitializeComponent();
            try
            {
                PropDict = DeserializeDictionaryFromXml("PropDict");
            }
            catch (System.IO.FileNotFoundException ex)
            {
                PropDict = new Dictionary<string, dynamic>() { 
                    { "inflationCoeff", 1.32 }, 
                    { "SGNTcoeff", true }, 
                    { "newCoeffAccept", true }, 
                    { "MotivatingCoeff", 0 },
                    { "IsPrintedIfHaventFee", true },
                    { "IsPrintedIfZero", true},
                    {"IsMotivationAccept", true},
                    {"isInflationCoeff", true},
                    {"IsPrintedWithCoeff", true}
                };
            }
            DataContext = PropDict;
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
            PropDict["inflationCoeff"] = InflationCoeff.Text;
            PropDict["SGNTcoeff"] = Convert.ToBoolean(SGNTcoeff.IsChecked);
            PropDict["newCoeffAccept"] = Convert.ToBoolean(newCoeffAccept.IsChecked);
            PropDict["MotivatingCoeff"] = MotivatingCoeff.SelectedIndex;
            PropDict["IsPrintedIfHaventFee"] = Convert.ToBoolean(IsPrintedIfHaventFee.IsChecked);
            PropDict["IsPrintedIfZero"] = Convert.ToBoolean(IsPrintedIfZero.IsChecked);
            PropDict["IsMotivationAccept"] = Convert.ToBoolean(IsMotivationAccept.IsChecked);
            PropDict["isInflationCoeff"] = Convert.ToBoolean(isInflationCoeff.IsChecked);
            PropDict["IsPrintedWithCoeff"] = Convert.ToBoolean(IsPrintedWithCoeff.IsChecked);

            SerializeDictionaryToXml(PropDict, "PropDict");
        }

        public static void SerializeDictionaryToXml(Dictionary<string, dynamic> dictionary, string filePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<DataEntry>));

            var dataEntries = new List<DataEntry>();
            foreach (var kvp in dictionary)
            {
                dataEntries.Add(new DataEntry { Key = kvp.Key, Value = kvp.Value });
            }

            using (var writer = new StreamWriter(filePath))
            {
                xmlSerializer.Serialize(writer, dataEntries);
            }
        }
        public static Dictionary<string, dynamic> DeserializeDictionaryFromXml(string filePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<DataEntry>));

            using (var reader = new StreamReader(filePath))
            {
                var dataEntries = (List<DataEntry>)xmlSerializer.Deserialize(reader);
                var dictionary = new Dictionary<string, dynamic>();

                foreach (var entry in dataEntries)
                {
                    dictionary[entry.Key] = entry.Value;
                }

                return dictionary;
            }
        }
    }
}
