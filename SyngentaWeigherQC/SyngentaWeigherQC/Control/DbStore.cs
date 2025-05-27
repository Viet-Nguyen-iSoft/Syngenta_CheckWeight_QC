using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyngentaWeigherQC.Models
{
  public class CommonDBContext : DbContext
  {
    //public string Server = "localhost";
    //public int Port = 5433;
    //public string User = "postgres";
    //public string Passwords = "058200005781";
    //public string DbDatalog = "DB_Syngenta_CheckWeight_V2";
    //public bool IsPostgres = true;

    //public string Server = "localhost";
    //public int Port = 5432;
    //public string User = "postgres";
    //public string Passwords = "isoft2025";
    //public string DbDatalog = "DB_CheckWeightQc";
    //public bool IsPostgres = true;

    public string Server = "localhost";
    public int Port = 5432;
    public string User = "postgres";
    public string Passwords = "isoft2025";
    public string DbDatalog = "DB_CheckWeightV2";
    public bool IsPostgres = true;
  }


  public class ConfigDBContext : CommonDBContext
  {
    public string DBPath = Application.StartupPath;
    

    //Danh Sách Bảng DB
    public DbSet<DatalogWeight> DatalogWeights { get; set; }
    public DbSet<DatalogTare> DatalogTares { get; set; }
    public DbSet<Production> Productions { get; set; }
    public DbSet<ShiftLeader> ShiftLeaders { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<ShiftType> ShiftTypes { get; set; }
    public DbSet<InforLine> InforLines { get; set; }
    public DbSet<HistoricalChangeMasterData> ProductionChangedLoggings { get; set; }
    public DbSet<ConfigSoftware> ConfigSoftwares { get; set; }
    public DbSet<Roles> Roles { get; set; }

    public ConfigDBContext()
    {
      DBPath += $"\\ConfigDB.sqlite";
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (IsPostgres)
      {
        optionsBuilder.UseNpgsql(
                                 $"Server={Server};" +
                                 $"Port={Port};" +
                                 $"Database={DbDatalog};" +
                                 $"User Id={User};" +
                                 $"Password={Passwords};"
                                 );
        optionsBuilder.EnableSensitiveDataLogging();
      }
      else
      {
        if (!optionsBuilder.IsConfigured)
        {
          optionsBuilder.UseSqlite($"Data Source={DBPath}");
        }
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
  }
}
