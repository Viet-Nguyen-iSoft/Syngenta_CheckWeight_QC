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
  public class ResponsitoryShiftLeader : GenericRepository<ShiftLeader, ConfigDBContext>
  {
    public ResponsitoryShiftLeader(DbContext context) : base(context)
    {

    }


    public async Task<List<ShiftLeader>> GetList(bool contain_isDelete)
    {
      if (contain_isDelete)
      {
        return await this.Context.Set<ShiftLeader>()
        .OrderBy(x=>x.Id)
        .ToListAsync();
      }  
      else
      {
        return await this.Context.Set<ShiftLeader>()
       .Where(x => !x.IsDelete)
       .OrderBy(x => x.Id)
       .ToListAsync();
      }  
    }
  }
}
