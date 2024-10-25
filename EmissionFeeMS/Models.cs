using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace EmissionFeeMS
{
    public class EmissionFee
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
        public string? HazardClass { get; set; }
    }

    public class FeeTax
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public double Fee { get; set; }
        public string? Pollutant { get; set; }
    }

    public class Coeff
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Value { get; set; }
    }

    public class CalcResult : INotifyPropertyChanged
    {
        string? code;
        string? title;
        double mass;
        double inflationCoeff = ApplicationProperty.AppProp.IsInflationCoeff ? ApplicationProperty.AppProp.InflationCoeff : 1;
        double _SGNTcoeff = ApplicationProperty.AppProp.SGNTcoeff ? 2 : 1;
        double motivatingCoeff = ApplicationProperty.AppProp.IsMotivationAccept ? new List<double>() { 25, 100 }[ApplicationProperty.AppProp.MotivatingCoeff] : 1;
        double fee;
        double result;

        public string? Code
        {
            get => code;
            set
            {
                code = value;
                OnPropertyChanged(nameof(Code));
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(Fee));
                OnPropertyChanged(nameof(Result));
            }
        }
        public string? Title
        {
            get
            {
                using var context = new ApplicationContext();
                context.EmissionFees.Load();
                return context.EmissionFees.Where(x => x.Code == code).Select(x => x.Title).FirstOrDefault();
            }
            private set
            {
                using ApplicationContext context = new();
                context.EmissionFees.Load();
                title = context.EmissionFees.Where(x => x.Code == code).Select(x => x.Title).FirstOrDefault();
                OnPropertyChanged(nameof(Title));
            }
        }
        public double Mass
        {
            get => mass;
            set
            {
                mass = value;
                OnPropertyChanged(nameof(Mass));
                OnPropertyChanged(nameof(Result));
            }
        }

        public double InflationCoeff
        {
            get => inflationCoeff;
            set
            {
                inflationCoeff = value;
                OnPropertyChanged(nameof(InflationCoeff));
                OnPropertyChanged(nameof(Result));
            }
        }

        public double SGNTcoeff
        {
            get => _SGNTcoeff;
            set
            {
                _SGNTcoeff = value;
                OnPropertyChanged(nameof(SGNTcoeff));
                OnPropertyChanged(nameof(Result));
            }
        }

        public double MotivatingCoeff
        {
            get => motivatingCoeff;
            set
            {
                motivatingCoeff = value;
                OnPropertyChanged(nameof(MotivatingCoeff));
                OnPropertyChanged(nameof(Result));
            }
        }

        public double Fee
        {
            get
            {
                using ApplicationContext context = new();
                context.FeeTaxes.Load();
                return context.FeeTaxes.Where(x => x.Pollutant.Contains(code)).Select(x => x.Fee).FirstOrDefault();
            }
            set
            {
                using var context = new ApplicationContext();
                context.FeeTaxes.Load();
                fee = context.FeeTaxes.Where(x => x.Pollutant.Contains(code)).Select(x => x.Fee).FirstOrDefault();
                OnPropertyChanged(nameof(Fee));
                OnPropertyChanged(nameof(Result));
            }
        }
        public double Result
        {
            get => Math.Round(Mass * Fee * InflationCoeff * SGNTcoeff * MotivatingCoeff, 2);
            private set
            {
                result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DataEntry
    {
        public string? Key { get; set; }
        public dynamic? Value { get; set; }
    }

    public class BoolOrDouble
    {
        public bool? BoolValue { get; set; }
        public double? DoubleValue { get; set; }

        public BoolOrDouble(bool value)
        {
            BoolValue = value;
            DoubleValue = null;
        }

        public BoolOrDouble(double value)
        {
            DoubleValue = value;
            BoolValue = null;
        }
    }

    public class AppProperty : INotifyPropertyChanged
    {
        double _InflationCoeff;
        bool _IsInflationCoeff;
        bool _SGNTcoeff;
        bool _NewCoeffAccept;
        int _MotivatingCoeff;
        bool _IsPrintedIfHaventFee;
        bool _IsPrintedIfZero;
        bool _IsMotivationAccept;
        bool _IsPrintedWithCoeff;

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
            get => _IsInflationCoeff; set
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
        public bool IsMotivationAccept
        {
            get => _IsMotivationAccept;
            set
            {
                _IsMotivationAccept = value;
                OnPropertyChanged(nameof(IsMotivationAccept));
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

    public static class ApplicationProperty
    {
        public static AppProperty AppProp = Load();
        public static void Save() => File.WriteAllText("AppProperty", JsonSerializer.Serialize(AppProp));
        public static AppProperty Load() => JsonSerializer.Deserialize<AppProperty>(new FileStream("AppProperty", FileMode.OpenOrCreate));
    }
}
