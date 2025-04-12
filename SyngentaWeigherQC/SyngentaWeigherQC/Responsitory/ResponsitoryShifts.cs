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
  internal class ResponsitoryShifts:GenericRepository<Shift, ConfigDBContext>
  {
    public ResponsitoryShifts(DbContext context) : base(context)
    {

    }

    //public override async Task<bool> UpdateInforShift(Shift shift)
    //{
    //  Context.Database.BeginTransaction();
    //  try
    //  {
    //    this.Context.Set<ShiftTypes>().UpdateRange(shiftTypes);
    //    await this.Context.SaveChangesAsync();
    //    Context.Database.CommitTransaction();
    //    return true;
    //  }
    //  catch (Exception ex)
    //  {
    //    Context.Database.RollbackTransaction();
    //    throw ex;
    //  }
    //}


  }
}
