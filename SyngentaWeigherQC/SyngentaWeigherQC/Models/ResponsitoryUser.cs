using Microsoft.EntityFrameworkCore;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Responsitory
{
  public class ResponsitoryUser : GenericRepository<ShiftLeader, ConfigDBContext>
  {

    public ResponsitoryUser(DbContext context) : base(context)
    {

    }

    

    public override async Task<bool> UpdateUserChoose(List<ShiftLeader> data)
    {
      Context.Database.BeginTransaction();
      try
      {
        this.Context.Set<ShiftLeader>().UpdateRange(data);
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


    //public override async Task<bool> UpdateRole(eAccount nameAccount, string role)
    //{
    //  Context.Database.BeginTransaction();
    //  try
    //  {
    //    var existed = await this.Context.Set<User>().FirstOrDefaultAsync(s => s.Account == nameAccount.ToString());
    //    if (existed != null)
    //    {
    //      existed.Role = role;  
    //      await this.Context.SaveChangesAsync();
    //      Context.Database.CommitTransaction();
    //      return true;
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    Context.Database.RollbackTransaction();
    //    throw ex;
    //  }
    //  return false;
    //}




  }
}
