using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        double inflationCoeff = 1;
        double _SGNTcoeff = 1;
        double motivatingCoeff = 1;
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

}
