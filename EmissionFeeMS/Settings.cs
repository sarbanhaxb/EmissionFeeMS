using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace EmissionFeeMS
{
    public class Settings
    {
        public void Save()
        {
            string filename = Global.SettingsFile;

            if (File.Exists(filename)) File.Delete(filename);

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, this);
                fs.Close();
            }
        }

        public static Settings GetSettings()
        {
            Settings? settings = null;
            string filename = Global.SettingsFile;

            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    settings = JsonSerializer.Deserialize<Settings>(fs);
                    fs.Close();
                }
            }
            else settings = new Settings();

            return settings;
        }


        public double InflationCoeff { get; set; }
        public Visibility IsInflationCoeff { get; set; }
        public Visibility SGNTcoeff { get; set; }
        public Visibility IsMotivationAccept { get; set; }
        public bool NewCoeffAccept { get; set; }
        public int MotivatingCoeff { get; set; }
        public bool IsPrintedIfHaventFee { get; set; }
        public bool IsPrintedIfZero { get; set; }
        public bool IsPrintedWithCoeff { get; set; }

        //public double InflationCoeff
        //{
        //    get => _InflationCoeff;
        //    set
        //    {
        //        _InflationCoeff = value;
        //        OnPropertyChanged(nameof(InflationCoeff));
        //    }
        //}
        //public Visibility IsInflationCoeff
        //{
        //    get => _IsInflationCoeff; set
        //    {
        //        _IsInflationCoeff = value;
        //        OnPropertyChanged(nameof(IsInflationCoeff));
        //    }
        //}
        //public Visibility SGNTcoeff
        //{
        //    get => _SGNTcoeff; set
        //    {
        //        _SGNTcoeff = value;
        //        OnPropertyChanged(nameof(SGNTcoeff));
        //    }
        //}
        //public Visibility IsMotivationAccept
        //{
        //    get => _IsMotivationAccept;
        //    set
        //    {
        //        _IsMotivationAccept = value;
        //        OnPropertyChanged(nameof(IsMotivationAccept));
        //    }
        //}
        //public bool NewCoeffAccept
        //{
        //    get => _NewCoeffAccept; set
        //    {
        //        _NewCoeffAccept = value;
        //        OnPropertyChanged(nameof(NewCoeffAccept));
        //    }
        //}
        //public int MotivatingCoeff
        //{
        //    get => _MotivatingCoeff;
        //    set
        //    {
        //        _MotivatingCoeff = value;
        //        OnPropertyChanged(nameof(MotivatingCoeff));
        //    }
        //}
        //public bool IsPrintedIfHaventFee
        //{
        //    get => _IsPrintedIfHaventFee; set
        //    {
        //        _IsPrintedIfHaventFee = value;
        //        OnPropertyChanged(nameof(IsPrintedIfHaventFee));
        //    }
        //}
        //public bool IsPrintedIfZero
        //{
        //    get => _IsPrintedIfZero;
        //    set
        //    {
        //        _IsPrintedIfZero = value;
        //        OnPropertyChanged(nameof(IsPrintedIfZero));
        //    }
        //}

        //public bool IsPrintedWithCoeff
        //{
        //    get => IsPrintedIfHaventFee;
        //    set
        //    {
        //        _IsPrintedWithCoeff = value;
        //        OnPropertyChanged(nameof(IsPrintedIfHaventFee));
        //    }
        //}

        //public event PropertyChangedEventHandler? PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}
