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
  internal class ResponsitoryShiftTypes: GenericRepository<ShiftType, ConfigDBContext>
  {
    public ResponsitoryShiftTypes(DbContext context) : base(context)
    {
    }

    public override async Task<bool> UpdateShiftTypesChoose(List<ShiftType> shiftTypes)
    {
      Context.Database.BeginTransaction();
      try
      {
        this.Context.Set<ShiftType>().UpdateRange(shiftTypes);
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
