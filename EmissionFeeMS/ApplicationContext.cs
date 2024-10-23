using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmissionFeeMS
{
    public class ApplicationContext : DbContext
    {
        public DbSet<EmissionFee> EmissionFees { get; set; } = null!;
        public DbSet<FeeTax> FeeTaxes { get; set; } = null!;
        public DbSet<Coeff> COEFFICIENT { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }


    //Для копирования ставок платы из старой таблицы в новую
    public class ApplicationContextOLD : DbContext
    {
        public DbSet<EmissionFee> EmissionFees { get; set; } = null!;
        public DbSet<FeeTax> feetaxes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=databaseOLD.db");
        }
    }
}
