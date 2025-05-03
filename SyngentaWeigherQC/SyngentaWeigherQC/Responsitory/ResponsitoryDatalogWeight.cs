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
  public class ResponsitoryDatalogWeight : GenericRepository<DatalogWeight, ConfigDBContext>
  {
    public ResponsitoryDatalogWeight(DbContext context) : base(context)
    {

    }

    public async Task<List<DatalogWeight>> GetList()
    {
      return await this.Context.Set<DatalogWeight>()
        .Include(x => x.Production)
        .Include(x => x.InforLine)
        .Include(x => x.Shift)
        .ToListAsync();
    }

    public async Task<DatalogWeight> GetProductById(int Id)
    {
      return await this.Context.Set<DatalogWeight>()
        .Include(x=>x.Production)
        .Where(x => x.Id== Id)
        .FirstOrDefaultAsync();
    }

    public async Task<List<DatalogWeight>> GetProductByDate(int LineId, DateTime from, DateTime to, int shiftId)
    {
      if (LineId==0)
      {
        if (shiftId==0)
        {
          return await this.Context.Set<DatalogWeight>()
            .Include(x => x.DatalogTare)
            .Include(x => x.Production)
            .Include(x => x.Shift)
            .Include(x => x.InforLine)
            .Include(x=>x.ShiftLeader)
            .Include(x=>x.ShiftType)
            .Where(x => x.CreatedAt >= from &&
                        x.CreatedAt <= to
                        )
            .OrderBy(x => x.Id)
            .ToListAsync();
        }
        else
        {
          return await this.Context.Set<DatalogWeight>()
            .Include(x => x.DatalogTare)
            .Include(x => x.Production)
            .Include(x => x.Shift)
            .Include(x=>x.InforLine)
            .Include(x => x.ShiftLeader)
            .Include(x => x.ShiftType)
            .Where(x => x.CreatedAt >= from &&
                        x.CreatedAt <= to &&
                        x.ShiftId == shiftId
                        )
            .OrderBy(x => x.Id)
            .ToListAsync();
        }  
      }
      else
      {
        if (shiftId == 0)
        {
          return await this.Context.Set<DatalogWeight>()
          .Include(x => x.DatalogTare)
          .Include(x => x.Production)
          .Include(x => x.Shift)
          .Include(x=>x.InforLine)
          .Include(x => x.ShiftLeader)
          .Include(x => x.ShiftType)
          .Where(x => x.CreatedAt >= from &&
                      x.CreatedAt <= to &&
                      x.InforLineId == LineId
                      )
          .OrderBy(x => x.Id)
          .ToListAsync();
        }
        else
        {
          return await this.Context.Set<DatalogWeight>()
          .Include(x => x.DatalogTare)
          .Include(x => x.Production)
          .Include(x => x.Shift)
          .Include(x => x.InforLine)
          .Include(x => x.ShiftLeader)
          .Include(x => x.ShiftType)
          .Where(x => x.CreatedAt >= from &&
                      x.CreatedAt <= to &&
                      x.ShiftId == shiftId && 
                      x.InforLineId == LineId
                      )
          .OrderBy(x => x.Id)
          .ToListAsync();
        }  
      }  
    }

  }
}
