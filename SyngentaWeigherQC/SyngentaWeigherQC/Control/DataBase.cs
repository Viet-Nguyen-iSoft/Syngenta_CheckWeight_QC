using Microsoft.EntityFrameworkCore;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Control
{
  public class DataBase
  {
    private static DataBase _ins;

    public static DataBase Ins
    {
      get { return _ins == null ? _ins = new DataBase() : _ins; }
    }
    public static string DailyDbPath { get; set; }
    public static string ConfigDbPath { get; set; } = $"./configDb.sqlite";

    //public static 
    public static async Task<bool> Init()
    {
      // Tạo DB Config
      using (var db = new ConfigDBContext())
      {
        try
        {
          await db.Database.EnsureCreatedAsync();
          await db.Database.BeginTransactionAsync();

          ////Roles
          //if (db.Roles.Count() <= 0)
          //{
          //  await db.Roles.AddRangeAsync(new Roles[]
          //  {
          //    new Roles(){ Name="QC", Description="Nhân viên kiểm soát chất lượng", Passwords="11",  CreatedAt=DateTime.Now, UpdatedAt=DateTime.Now, Permission="", isEnable=false},
          //    new Roles(){ Name="ME", Description="Nhân viên cơ điện", Passwords="22", CreatedAt=DateTime.Now, UpdatedAt=DateTime.Now, Permission="", isEnable=false},
          //    new Roles(){ Name="ShiftLeader", Description="Trưởng ca", Passwords="33", CreatedAt=DateTime.Now, UpdatedAt=DateTime.Now, Permission="", isEnable=false},
          //    new Roles(){ Name="OP", Description="Công nhân vận hành", Passwords="44", CreatedAt=DateTime.Now, UpdatedAt=DateTime.Now, Permission="", isEnable=true},
          //    new Roles(){ Name="Admin", Description="Admin", Passwords="55", CreatedAt=DateTime.Now, UpdatedAt=DateTime.Now, Permission="", isEnable=false},
          //  });
          //}
          //await db.SaveChangesAsync();

          ////SerialControllers
          //if (db.SerialControllers.Count() <= 0)
          //{
          //  await db.SerialControllers.AddRangeAsync(new SerialControllers[]
          //  {
          //      new SerialControllers(){ Name="satorious", Serial="satorious", COM="COM1", Baud=9600, Parity=Parity.Odd, Databits=DataBits.Seven, Stopbits=StopBits.One, CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
          //  });
          //}
          //await db.SaveChangesAsync();


          //Shift Type
          if (db.ShiftTypes.Count() <= 0)
          {
            await db.ShiftTypes.AddRangeAsync(new ShiftType[]
            {
              new ShiftType(){Id=3,Code = 3, Name="Hành chính", isEnable=true},
              new ShiftType(){Id=2,Code = 2, Name="Giãn ca", isEnable=true},
              new ShiftType(){Id=1,Code = 1, Name="3 ca", isEnable=true},
            });
          }
          await db.SaveChangesAsync();

          //Shift
          if (db.Shifts.Count() <= 0)
          {
            await db.Shifts.AddRangeAsync(new Shift[]
            {
              new Shift(){Id=6, Name="Hành chính", Description="Hành chính", StartHour=8, StartMinute=0,StartSecond=0, EndHour=16,EndMinute=0,EndSecond=0, Hours=8,CodeShift=6, ShiftTypeId=3,CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
              new Shift(){Id=5, Name="Giãn ca 3", Description="Giãn ca 3", StartHour=18,StartMinute=0,StartSecond=0, EndHour=6,EndMinute=0,EndSecond=0, Hours=12,CodeShift=5, ShiftTypeId=2,CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
              new Shift(){Id=4, Name="Giãn ca 1", Description="Giãn ca 1", StartHour=6,StartMinute=0,StartSecond=0, EndHour=18,EndMinute=0,EndSecond=0, Hours=12,CodeShift=4, ShiftTypeId=2,CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
              new Shift(){Id=3, Name="Ca 3", Description="Shift 3", StartHour=22,StartMinute=0,StartSecond=0, EndHour=6,EndMinute=0,EndSecond=0, Hours=8,CodeShift=3, ShiftTypeId=1,CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
              new Shift(){Id=2, Name="Ca 2", Description="Shift 2", StartHour=14,StartMinute=0,StartSecond=0, EndHour=22,EndMinute=0,EndSecond=0, Hours=8,CodeShift=2, ShiftTypeId=1,CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
              new Shift(){Id=1, Name="Ca 1", Description="Shift 1", StartHour=6,StartMinute=0,StartSecond=0, EndHour=14,EndMinute=0,EndSecond=0, Hours=8,CodeShift=1, ShiftTypeId=1,CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
            });
          }
          await db.SaveChangesAsync();



          //Station
          if (db.InforLines.Count() <= 0)
          {
            await db.InforLines.AddRangeAsync(new InforLine[]
            {
              new InforLine(){ Name="KL6", Code="KL6", PathReportLocal="",PassReport="1", CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now},
              new InforLine(){Name = "KL5", Code = "KL5", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
              new InforLine(){Name = "KL4", Code = "KL4", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
              new InforLine(){Name = "KL3", Code = "KL3", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
              new InforLine(){Name = "KL2", Code = "KL2", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
              new InforLine(){Name = "KL1", Code = "KL1", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
              new InforLine(){Name = "GN5", Code = "GN5", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
              new InforLine(){Name = "GN6", Code = "GN6", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
              new InforLine(){Name = "GN7", Code = "GN7", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
              new InforLine(){Name = "GN8", Code = "GN8", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, IsEnable=true},
              new InforLine(){Name = "GN9", Code = "GN9", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, IsEnable=true},
              new InforLine(){Name = "Sachet", Code = "Sachet", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
              new InforLine(){Name = "SBL", Code = "SBL", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
              new InforLine(){Name = "CUP", Code = "CUP", PathReportLocal = "", PassReport = "1", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},

            });
          }
          await db.SaveChangesAsync();

          //ConfigSoftwares
          if (db.ConfigSoftwares.Count() <= 0)
          {
            await db.ConfigSoftwares.AddRangeAsync(new ConfigSoftware[]
            {
              new ConfigSoftware(){ NameStation="TRẠM 1", IpTcp="127.0.0.1", PortTcp=2305, Spare1=0,Spare2="", Spare3="1"},
            });
          }
          await db.SaveChangesAsync();

          db.Database.CommitTransaction();

          return true;
        }
        catch (Exception ex)
        {
          db.Database.RollbackTransaction();
          throw ex;
        }
      }
    }
    public Task<bool> Add<T>(DbContext db, IEnumerable<T> objects) where T : class
    {
      return Task.Run(() =>
      {
        try
        {
          db.Database.BeginTransaction();
          var dbset = db.Set<T>();
          foreach (var ob in objects)
          {
            var exist = dbset.Find(ob);
            if (exist != null) continue;
            dbset.Add(ob);
          }
          db.SaveChanges();
          db.Database.CommitTransaction();
          return true;
        }
        catch (Exception ex)
        {
          db.Database.RollbackTransaction();
          throw (ex);
        }
      });
    }

    public Task<bool> Add<T>(DbContext db, T objects) where T : class
    {
      return Task.Run(() =>
      {
        try
        {
          db.Database.BeginTransaction();
          var dbset = db.Set<T>();
          var exist = dbset.Find(objects);
          if (exist != null) throw new Exception("Entity has existed");
          dbset.Add(objects);

          db.SaveChanges();
          db.Database.CommitTransaction();
          return true;
        }
        catch (Exception ex)
        {
          db.Database.RollbackTransaction();
          throw (ex);
        }
      });
    }

    public Task<bool> Delete<T>(DbContext db, IEnumerable<T> objects) where T : class
    {
      return Task.Run(() =>
      {
        try
        {
          db.Database.BeginTransaction();
          var dbset = db.Set<T>();
          foreach (var ob in objects)
          {
            var exist = dbset.Find(ob);
            if (exist == null) continue;
            dbset.Remove(ob);
          }
          db.SaveChanges();
          db.Database.CommitTransaction();
          return true;
        }
        catch (Exception ex)
        {
          db.Database.RollbackTransaction();
          throw (ex);
        }
      });
    }

    public Task<bool> Delete<T>(DbContext db, T objects) where T : class
    {
      return Task.Run(() =>
      {
        try
        {
          db.Database.BeginTransaction();
          var dbset = db.Set<T>();
          var exist = dbset.Find(objects);
          if (exist == null) return true;
          dbset.Remove(objects);
          db.SaveChanges();
          db.Database.CommitTransaction();
          return true;
        }
        catch (Exception ex)
        {
          db.Database.RollbackTransaction();
          throw (ex);
        }
      });
    }
    public Task<bool> Update<T>(DbContext db, IEnumerable<T> objects) where T : class
    {
      return Task.Run(() =>
      {
        try
        {
          db.Database.BeginTransaction();
          var dbset = db.Set<T>();
          foreach (var ob in objects)
          {
            var exist = dbset.Find(ob);
            if (exist == null)
            {
              dbset.Add(ob);
            }
            dbset.Update(ob);
          }
          db.SaveChanges();
          db.Database.CommitTransaction();
          return true;
        }
        catch (Exception ex)
        {
          db.Database.RollbackTransaction();
          throw (ex);
        }
      });
    }

    public Task<T> Update<T>(DbContext db, T objects) where T : class
    {
      return Task.Run(() =>
      {
        try
        {
          db.Database.BeginTransaction();
          var dbset = db.Set<T>();
          var exist = dbset.Find(objects);
          if (exist == null)
          {
            dbset.Add(objects);
          }
          dbset.Update(objects);
          db.SaveChanges();
          db.Database.CommitTransaction();
          return dbset.Find(objects);
        }
        catch (Exception ex)
        {
          db.Database.RollbackTransaction();
          throw (ex);
        }
      });
    }
  }
}
