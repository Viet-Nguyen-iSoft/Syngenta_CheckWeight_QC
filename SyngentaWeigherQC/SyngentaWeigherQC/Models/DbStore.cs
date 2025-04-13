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
    public string Server = "localhost";
    public int Port = 5433;
    public string User = "postgres";
    public string Passwords = "058200005781";
    public string DbDatalog = "DB_Syngenta_CheckWeight";
    public bool IsPostgres = true;
  }
 

  public class ConfigDBContext : CommonDBContext
  {
    public DbSet<HistoricalChangeMasterData> ProductionChangedLoggings { get; set; }
    public DbSet<Production> Productions { get; set; }
    public DbSet<ShiftLeader> Users { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<SerialControllers> SerialControllers { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<ShiftType> ShiftTypes { get; set; }
    public DbSet<InforLine> InforLines { get; set; }
    public DbSet<InforValueSettingStation> InforValueSettingStations { get; set; }



    public string DailyDbPath = Application.StartupPath;
    public DbSet<DatalogWeight> Datalogs { get; set; }
    public DbSet<Sample> Samples { get; set; }
    public DbSet<Tare> Tares { get; set; }

    public string ConfigDBPath = Application.StartupPath;
    public ConfigDBContext()
    {
      ConfigDBPath += $"\\ConfigDB.sqlite";
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
          optionsBuilder.UseSqlite($"Data Source={DailyDbPath}");
        }
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
  }
}
