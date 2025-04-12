using Microsoft.EntityFrameworkCore;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Responsitory
{
  public class ResponsitoryDatalog : GenericRepository<Datalog, ConfigDBContext>
  {
    public ResponsitoryDatalog(DbContext context) : base(context)
    {

    }

    public override async Task<bool> UpdateDatalog(Datalog datalog)
    {
      Context.Database.BeginTransaction();
      try
      {
        this.Context.Set<Datalog>().Update(datalog);
        await this.Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
        return true;
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
        return false;
      }
    }

    public override async Task<List<Datalog>> LoadAllDatalogsByGroupId(int groupId)
    {
      Context.Database.BeginTransaction();
      try
      {
        List<Datalog> data =await this.Context.Set<Datalog>().Where(x=>x.GroupId == groupId).ToListAsync();
        await this.Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
        return data;
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
        return null;
      }
    }

    public override async Task<List<Datalog>> LoadAllDatalogsByProductId(int productId)
    {
      Context.Database.BeginTransaction();
      try
      {
        List<Datalog> data = await this.Context.Set<Datalog>().Where(x => x.ProductId == productId).ToListAsync();
        await this.Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
        return data;
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
        return null;
      }
    }

    public override async Task<bool> CheckGroupIdDatalog(int groupId)
    {
      Context.Database.BeginTransaction();
      try
      {
        Datalog data = this.Context.Set<Datalog>().Where(x => x.GroupId == groupId).FirstOrDefault();
        await this.Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
        return (data != null);
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
        return false;
      }
    }


    public override async Task<List<Datalog>> LoadDatalogsByShiftId(int shiftId)
    {
      Context.Database.BeginTransaction();
      try
      {
        List<Datalog> data = await this.Context.Set<Datalog>().Where(x => x.ShiftId == shiftId).ToListAsync();
        await this.Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
        return data;
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
        return null;
      }
    }












  }
}
