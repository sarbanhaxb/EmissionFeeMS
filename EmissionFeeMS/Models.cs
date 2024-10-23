using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmissionFeeMS
{
    public class EmissionFee
    {
        public int Id {  get; set; }
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

    public class  CalcResult
    {
        public string? Code { get; set; }
        public string? Title { get; set; }
        public double Mass {  get; set; }
        public double Fee { get; set; }
        public double Result { get; set; }
    }
}
