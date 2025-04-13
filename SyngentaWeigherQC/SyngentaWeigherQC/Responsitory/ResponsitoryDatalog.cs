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
  public class ResponsitoryDatalog : GenericRepository<DatalogWeight, ConfigDBContext>
  {
    public ResponsitoryDatalog(DbContext context) : base(context)
    {

    }

    public override async Task<bool> UpdateDatalog(DatalogWeight datalog)
    {
      Context.Database.BeginTransaction();
      try
      {
        this.Context.Set<DatalogWeight>().Update(datalog);
        await this.Context.SaveChangesAsync();
        Context.Database.CommitTransaction();
        return true;
      }
      catch (Exception ex)
      {
        Context.Database.RollbackTransaction();
        throw ex;
      }
    }













  }
}
