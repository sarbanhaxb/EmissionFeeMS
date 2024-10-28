using System.ComponentModel;
using System.IO;
using System.Text.Json;

namespace EmissionFeeMS.SettingsMVVM
{
    public class Settings : INotifyPropertyChanged
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


        double _InflationCoeff { get; set; }
        bool _IsInflationCoeff { get; set; }
        bool _SGNTcoeff { get; set; }
        bool _IsMotivationAccept { get; set; }
        bool _NewCoeffAccept { get; set; }
        int _MotivatingCoeff { get; set; }
        bool _IsPrintedIfHaventFee { get; set; }
        bool _IsPrintedIfZero { get; set; }
        bool _IsPrintedWithCoeff { get; set; }

        public double InflationCoeff
        {
            get => _InflationCoeff;
            set
            {
                _InflationCoeff = value;
                OnPropertyChanged(nameof(InflationCoeff));
            }
        }
        public bool IsInflationCoeff
        {
            get => _IsInflationCoeff;
            set
            {
                _IsInflationCoeff = value;
                OnPropertyChanged(nameof(IsInflationCoeff));
            }
        }
        public bool SGNTcoeff
        {
            get => _SGNTcoeff; set
            {
                _SGNTcoeff = value;
                OnPropertyChanged(nameof(SGNTcoeff));
            }
        }
        public bool IsMotivationAccept
        {
            get => _IsMotivationAccept;
            set
            {
                _IsMotivationAccept = value;
                OnPropertyChanged(nameof(IsMotivationAccept));
            }
        }
        public bool NewCoeffAccept
        {
            get => _NewCoeffAccept; set
            {
                _NewCoeffAccept = value;
                OnPropertyChanged(nameof(NewCoeffAccept));
            }
        }
        public int MotivatingCoeff
        {
            get => _MotivatingCoeff;
            set
            {
                _MotivatingCoeff = value;
                OnPropertyChanged(nameof(MotivatingCoeff));
            }
        }
        public bool IsPrintedIfHaventFee
        {
            get => _IsPrintedIfHaventFee; set
            {
                _IsPrintedIfHaventFee = value;
                OnPropertyChanged(nameof(IsPrintedIfHaventFee));
            }
        }
        public bool IsPrintedIfZero
        {
            get => _IsPrintedIfZero;
            set
            {
                _IsPrintedIfZero = value;
                OnPropertyChanged(nameof(IsPrintedIfZero));
            }
        }

        public bool IsPrintedWithCoeff
        {
            get => IsPrintedIfHaventFee;
            set
            {
                _IsPrintedWithCoeff = value;
                OnPropertyChanged(nameof(IsPrintedIfHaventFee));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
