using Microsoft.EntityFrameworkCore;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Responsitory
{
  public class ResponsitorySamples : GenericRepository<Sample, ConfigDBContext>
  {
    public ResponsitorySamples(DbContext context) : base(context)
    {

    }

    public override async Task<bool> UpdateSample(Sample sample)
    {
      Context.Database.BeginTransaction();
      try
      {
        this.Context.Set<Sample>().Update(sample);
        await this.Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
        return true;
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        LoggerHelper.LogErrorToFileLog(ex);
        return false;
      }
    }

    public override async Task<List<Sample>> LoadAllSamplesByDatalogId(List<int> datalogId)
    {
      Context.Database.BeginTransaction();
      try
      {
        List<Sample> datas = new List<Sample>();
        if (datalogId.Count()>0)
        {
          foreach (int id in datalogId)
          {
            List<Sample> data = await this.Context.Set<Sample>().Where(x => x.DatalogId == id).ToListAsync();
            datas.AddRange(data);
          }
        }
        return datas;
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        LoggerHelper.LogErrorToFileLog(ex);
        return null;
      }
    }
    public override async Task<List<Sample>> LoadAllSamplesByGroupId(int groupId)
    {
      Context.Database.BeginTransaction();
      try
      {
        return await this.Context.Set<Sample>().Where(x => x.GroupId == groupId).ToListAsync();
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        LoggerHelper.LogErrorToFileLog(ex);
        return null;
      }
    }

    public override async Task<List<Sample>> LoadAllSamplesByGroupId_V2(int groupId)
    {
      Context.Database.BeginTransaction();
      try
      {
        return await this.Context.Set<Sample>().Where(x => x.GroupId == groupId && x.isEnable && x.isHasValue).ToListAsync();
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        LoggerHelper.LogErrorToFileLog(ex);
        return null;
      }
    }



  }
}
