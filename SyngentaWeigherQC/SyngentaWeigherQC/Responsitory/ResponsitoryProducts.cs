using Microsoft.EntityFrameworkCore;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Responsitory
{
  public class ResponsitoryProducts : GenericRepository<Production, ConfigDBContext>
  {
    public ResponsitoryProducts(DbContext context) : base(context)
    {

    }

    public override async Task<bool> UpdateRangeByIsDelete(List<Production> data)
    {
      Context.Database.BeginTransaction();
      try
      {
        foreach (var prod in data)
        {
          var existed = this.Context.Set<Production>().Where(s => s.IsDelete == false && s.Name== prod.Name).FirstOrDefault();
          if (existed != null)
          {
            existed.IsDelete = true;
            this.Context.Set<Production>().Update(existed);
          }
        }

        //var existed = await this.Context.Set<Productions>().Where(s => s.IsDelete == false).ToListAsync();
        //if (existed != null)
        //{
        //  foreach (var item in existed)
        //  {
        //    item.IsDelete = true;
        //  }
        //}
        ////this.Context.Set<MasterData>().Update(entity);
        await this.Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
        return true;
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.Message + "&" + ex.StackTrace);
        Context.Database.RollbackTransaction();
        return false;
      }
    }


    public override async Task<bool> UpdateProductChoose(List<Production> productions)
    {
      Context.Database.BeginTransaction();
      try
      {
        this.Context.Set<Production>().UpdateRange(productions);
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



    public override async Task<List<Production>> LoadAllProducts()
    {
      List<Production> datas = new List<Production>();
      try
      {
        datas = await this.Context.Set<Production>().Where(s => s.IsDelete == false).ToListAsync();
        return datas;
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
        return datas;
      }
    }

  }
}
