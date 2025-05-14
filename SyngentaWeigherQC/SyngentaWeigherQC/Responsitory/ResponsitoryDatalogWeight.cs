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

    public async Task<List<DatalogWeight>> GetProductByDate(int LineId, DateTime fromDate, DateTime toDate, int shiftId)
    {
      DateTime from = fromDate.Date + new TimeSpan(6, 0, 0);
      DateTime to = toDate.Date.AddDays(1) + new TimeSpan(5, 59, 59);

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
            .Where(x => x.CreatedAt.Date >= from.Date &&
                        x.CreatedAt.Date <= to.Date
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
            .Where(x => x.CreatedAt.Date >= from.Date &&
                        x.CreatedAt.Date <= to.Date &&
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
          .Where(x => x.CreatedAt.Date >= from.Date &&
                      x.CreatedAt.Date <= to.Date &&
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
          .Where(x => x.CreatedAt.Date >= from.Date &&
                      x.CreatedAt.Date <= to.Date &&
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
